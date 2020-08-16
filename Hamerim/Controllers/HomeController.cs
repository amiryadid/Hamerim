using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hamerim.Data;
using Hamerim.Models;

namespace Hamerim.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var ctx = new HamerimDbContext())
            {
                IList<Order> orders = ctx.Orders.ToList();
                Console.WriteLine(orders.First().ServicesInOrder);
                Console.WriteLine(orders.First().Club.ClubOrders);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}