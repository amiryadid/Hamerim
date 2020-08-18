using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hamerim.Data;
using Hamerim.Models;

namespace Hamerim.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        [NonAction]
        public List<Service> MostPopularServices(int month)
        {
            using (var ctx = new HamerimDbContext())
            {
                var groupedServices =
                    ctx.Orders.Where(order => order.Date.Month == month)
                        .SelectMany(order => order.ServicesInOrder)
                        .GroupBy(service => service.Id);

                return groupedServices.Where(group => group.Count() ==
                                                      groupedServices.Max(groupsToCount => groupsToCount.Count()))
                    .Select(group => ctx.Services.FirstOrDefault(service => service.Id == group.Key))
                    .ToList();
            }
        }
    }
}