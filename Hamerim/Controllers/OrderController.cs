using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hamerim.Data;
using Hamerim.Models;
using Hamerim.Services;

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
                ViewBag.Clubs = ctx.Clubs;
            }

            return View();
        }

        public ActionResult AfterChooseClub(int Id)
        {
            using (var ctx = new HamerimDbContext())
            {
                var chosenClub = ctx.Clubs.First(club => club.Id == Id);
                ViewBag.ChosenClub = chosenClub;
                ViewBag.UnavailableDates = chosenClub.ClubOrders.Where(club => club.Date >= DateTime.Today)
                                                                .Select(order => order.Date).ToList();
                ViewBag.ServiceCategories = 
                    ctx.ServiceCategories.Include(category => category.ServicesInCategory).ToList();

            }

            return View();
        }

        public ActionResult GetPopularServices(DateTime date)
        {
            List<Service> services = 
                this.statisticsService.GetMostPopularServices(date.Month).ToList();

            return Json(services);
        }
    }
}