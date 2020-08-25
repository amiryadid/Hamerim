using System.Collections.Generic;
using System.Linq;
using Hamerim.Data;
using Hamerim.Models;

namespace Hamerim.Services
{
    public class StatisticsService : IStatisticsService
    {
        public IEnumerable<Service> GetMostPopularServices(int month)
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

        public IEnumerable<dynamic> GetMonthlySales()
        {
            using (var ctx = new HamerimDbContext())
            {
                return ctx.Orders.GroupBy(order => order.Date.Month).AsEnumerable().Select(group => new
                {
                    Month = group.Key,
                    AmountOfOrders = group.Count(),
                    Profit = group.Sum(order => order.TotalCost())
                }).ToList();
            }
        }

        public IEnumerable<dynamic> OrdersByClub()
        {
            using (var ctx = new HamerimDbContext())
            {
                return ctx.Orders.GroupBy(order => order.Club).Select(group => new
                {
                    Club = group.Key,
                    AmountOfOrders = group.Count()
                }).ToList();
            }
        }
    }
}