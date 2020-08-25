using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hamerim.Data;
using Hamerim.Models;
using Hamerim.Services;
using Microsoft.Ajax.Utilities;

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
            }

            return View();
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
            List<Service> services = 
                this.statisticsService.GetMostPopularServices(month).ToList();

            return Json(services);
        }

        [HttpPost]
        public ActionResult BookOrder(Order order)
        {
            using (HamerimDbContext ctx = new HamerimDbContext())
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();
            }

            return RedirectToAction("FinishedOrder", order.Id);
        }

        public ActionResult FinishedOrder(int orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;

            return View();
        }
    }
}