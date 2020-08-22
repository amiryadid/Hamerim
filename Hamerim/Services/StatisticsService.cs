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
    }
}