using System.Collections.Generic;
using System.Linq;
using Hamerim.Data;
using Hamerim.Models;

namespace Hamerim.Services
{
    public class StatisticsService : IStatisticsService
    {
        public IEnumerable<int> GetMostPopularServices(int month)
        {
            using (var ctx = new HamerimDbContext())
            {
                var groupedServices =
                    ctx.Orders.Where(order => order.Date.Month == month)
                        .SelectMany(order => order.ServicesInOrder)
                        .GroupBy(service => service.Id);

                return groupedServices.Where(group => group.Count() ==
                                                      groupedServices.Max(groupsToCount => groupsToCount.Count()))
                    .Select(group => ctx.Services.FirstOrDefault(service => service.Id == group.Key).Id)
                    .ToList();
            }
        }

        public IEnumerable<dynamic> GetMonthlySales()
        {
            using (var ctx = new HamerimDbContext())
            {
                var data = ctx.Orders.GroupBy(order => order.Date.Month).AsEnumerable().Select(group => new
                {
                    Month = group.Key,
                    AmountOfOrders = group.Count(),
                    Profit = group.Sum(order => order.TotalCost())
                }).ToList();

                for(int month=1; month<=12; month++)
                {
                    if (!data.Any(entry => entry.Month == month))
                        data.Add(new
                        {
                            Month = month,
                            AmountOfOrders = 0,
                            Profit = 0
                        });
                }

                return data;
            }
        }

        public IEnumerable<dynamic> OrdersByClub()
        {
            using (var ctx = new HamerimDbContext())
            {
                var clubs = ctx.Clubs.ToList();

                var data = ctx.Orders.GroupBy(order => order.Club).AsEnumerable().Select(group => new
                {
                    Club = group.Key.Name,
                    AmountOfOrders = group.Count()
                }).ToList();

                clubs = clubs.Where(club => !data.Any(entry => entry.Club == club.Name)).ToList();

                data.AddRange(clubs.Select(club => new
                {
                    Club = club.Name,
                    AmountOfOrders = 0
                }).ToList());

                return data;
            }
        }
    }
}