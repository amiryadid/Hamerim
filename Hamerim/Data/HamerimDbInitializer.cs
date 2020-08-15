using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hamerim.Models;

namespace Hamerim.Data
{
    public class HamerimDbInitializer : DropCreateDatabaseAlways<HamerimDbContext>
    {
        protected override void Seed(HamerimDbContext context)
        {
            SeedServiceCategories(context);
            SeedServices(context);
            base.Seed(context);
        }

        private void SeedServiceCategories(HamerimDbContext context)
        {
            var category = new ServiceCategory() { Id = 1, Title = "Drinks" };
            context.ServiceCategories.Add(category);
            context.SaveChanges();
        }

        private void SeedServices(HamerimDbContext context)
        {
            IList<ServiceCategory> categories = context.ServiceCategories.ToList();
            ICollection<Service> services = new List<Service>()
            {
                new Service()
                {
                    Id = 1,
                    Category = categories.First(category => category.Title=="Drinks"), 
                    Cost = 50, 
                    Title = "Light Drinks Stand"
                },
                new Service()
                {
                    Id = 2,
                    Category = categories.First(category => category.Title=="Drinks"), 
                    Cost = 100, 
                    Title = "Alcohol Stand"
                }
            };

            context.Services.AddRange(services);
            context.SaveChanges();
        }
    }
}