using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hamerim.Models
{
    public class ServiceCategory
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Service> ServicesInCategory { get; set; }
    }
}