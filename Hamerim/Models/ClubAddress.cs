using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Hamerim.Models
{
    public class ClubAddress
    {
        [Key, ForeignKey("Club")]
        public int ClubId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public virtual Club Club { get; set; }
        public override string ToString()
        {
            return $"{this.City}, {this.Street} {this.HouseNumber}";
        }
    }
}