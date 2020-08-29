using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Hamerim.Data;
using Hamerim.Models;
using Hamerim.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hamerim.Controllers
{
    public class OrderController : Controller
    {
        readonly IStatisticsService statisticsService;

        public OrderController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        // GET: Order
        public ActionResult NewOrder()
        {
            using (var ctx = new HamerimDbContext())
            {
                ViewBag.Clubs = ctx.Clubs.Include(cl => cl.Address).ToList();
                ViewBag.Cities = ctx.ClubAddresses.Select(address => address.City).Distinct().ToList();
            }

            return View();
        }

        public async Task<ActionResult> GetLocations()
        {
            using (var ctx = new HamerimDbContext())
            {
                List<dynamic> locations = new List<dynamic>();
                dynamic[] results;

                var tasks = ctx.Clubs.ToList().Select(GetClubLocation);
                results = await Task.WhenAll(tasks);
                locations.AddRange(results
                    .Select(result => new
                    {
                        lat = JArray.Parse(result.Data)[0].Value<string>("lat"),
                        lon = JArray.Parse(result.Data)[0].Value<string>("lon"),
                        result.Club
                    }));

                return Json(locations, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FilterClubs(int maxPriceFilter=Int32.MaxValue, string nameFilter = "", string cityFilter = "")
        {
            using (var ctx = new HamerimDbContext())
            {
                ViewBag.Clubs = ctx.Clubs.Include(cl => cl.Address).ToList();

                IEnumerable<Club> filteredClubs = ViewBag.Clubs;

                if (!nameFilter.IsEmpty())
                    filteredClubs = filteredClubs.Where(club => club.Name.Contains(nameFilter));

                if (!cityFilter.IsEmpty())
                    filteredClubs = filteredClubs.Where(club => club.Address.City == cityFilter);

                ViewBag.Clubs = filteredClubs.Where(club => club.Cost <= maxPriceFilter).ToList();
            }

            return Json(ViewBag.Clubs);
        }

        public ActionResult AfterChooseClub(int Id)
        {
            using (var ctx = new HamerimDbContext())
            {
                var chosenClub = ctx.Clubs.Include(cl => cl.Address).First(club => club.Id == Id);
                ViewBag.ChosenClub = chosenClub;
                ViewBag.UnavailableDates = chosenClub.ClubOrders.Where(club => club.Date >= DateTime.Today)
                                                                .Select(order => order.Date).ToList();
                ViewBag.ServiceCategories = 
                    ctx.ServiceCategories.Include(category => category.ServicesInCategory).ToList();
            }

            return View();
        }

        public ActionResult GetPopularServices(int month)
        {
            List<Service> services = statisticsService.GetMostPopularServices(month).ToList();
            
            return Json(services);
        }

        [HttpPost]
        public ActionResult BookOrder(int clubId, string clientName, string clientPhone, string txtDateTime, List<int> serviceIds)
        {
            using (HamerimDbContext ctx = new HamerimDbContext())
            {
                Order newOrder = new Order
                {
                    Date = DateTime.ParseExact(txtDateTime, "MM/dd/yyyy", null),
                    Club = ctx.Clubs.Find(clubId),
                    ClientName = clientName,
                    ClientPhone = clientPhone,
                    ServicesInOrder = serviceIds != null ? serviceIds.Select(id => ctx.Services.Find(id)).ToList() : null
                };

                ctx.Orders.Add(newOrder);
                ctx.SaveChanges();
                
                return RedirectToAction("FinishedOrder", new { orderNumber = newOrder.Id });
            }
        }

        public ActionResult FinishedOrder(int orderNumber)
        {
            using (var ctx = new HamerimDbContext())
            {
                ViewBag.Order = ctx.Orders
                    .Include(order => order.Club)
                    .Include(order => order.Club.Address)
                    .Include(order => order.ServicesInOrder)
                    //.Include("Order.ServicesInOrder.Category")
                    .FirstOrDefault(order => order.Id == orderNumber);
            }

            return View();
        }

        private async Task<dynamic> GetClubLocation(Club club)
        {
            using (var client = new HttpClient())
            {
                Thread.Sleep(1000); // Because API limits requests per second

                string apiToken = ConfigurationManager.AppSettings["LocationIQApiToken"];
                string url =
                    "https://eu1.locationiq.com/v1/search.php?key={0}&country=Israel&city={1}&street={2}&addressdetails=1&format=json";
                url += club.Address.HouseNumber != 0 ? "&house_number=" + club.Address.HouseNumber.ToString() : "";
                var uri = new Uri(string.Format(url, apiToken, club.Address.City, club.Address.Street));

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();

                return new
                {
                    Club = club.Name,
                    Data = textResult
                };
            }
        }
    }
}