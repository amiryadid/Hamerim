﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hamerim.Models
{
    public class Order
    {
        public Order()
        {
            this.ServicesInOrder = new HashSet<Service>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName="Date")]
        public DateTime Date { get; set; }
        
        [Required]
        public virtual Club Club { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ClientPhone { get; set; }

        public virtual ICollection<Service> ServicesInOrder { get; set; }

        public int TotalCost()
        {
            return Club.Cost + ServicesInOrder.Sum(serviceInOrder => serviceInOrder.Cost);
        }
    }
}