﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hamerim.Models
{
    public class ServiceCategory
    {
        public ServiceCategory()
        {
            this.ServicesInCategory = new HashSet<Service>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Service> ServicesInCategory { get; set; }
    }
}