using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hamerim.Models
{
    public class Club
    {
        public Club()
        {
            this.ClubOrders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public virtual ClubAddress Address { get; set; }

        public virtual ICollection<Order> ClubOrders { get; set; }
    }
}