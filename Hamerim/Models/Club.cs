using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public virtual ClubAddress Address { get; set; }

        public virtual ICollection<Order> ClubOrders { get; set; }
    }
}