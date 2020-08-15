using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hamerim.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Cost { get; set; }

        [Required]
        public virtual ServiceCategory Category { get; set; }
    }
}