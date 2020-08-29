using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamerim.Models;

namespace Hamerim.Services
{
    public interface IStatisticsService
    {
        IEnumerable<Service> GetMostPopularServices(int month);
        
        IEnumerable<dynamic> GetMonthlySales();
        
        IEnumerable<dynamic> OrdersByClub();
    }
}
