using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            SeedClubs(context);
            SeedClubAddresses(context);
            SeedOrders(context);
            base.Seed(context);
        }

        private void SeedServiceCategories(HamerimDbContext context)
        {
            ICollection<ServiceCategory> serviceCategories = new List<ServiceCategory>()
            {
                new ServiceCategory()
                {
                    Id = 1,
                    Title = "Drinks"
                },
                new ServiceCategory()
                {
                    Id = 2,
                    Title = "Food"
                }
            };

            context.ServiceCategories.AddRange(serviceCategories);
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
                    Title = "Light Drinks Fridge"
                },
                new Service()
                {
                    Id = 2,
                    Category = categories.First(category => category.Title=="Drinks"), 
                    Cost = 100, 
                    Title = "Alcohol Bar"
                },
                new Service()
                {
                    Id = 3,
                    Category = categories.First(category => category.Title=="Food"),
                    Cost = 200,
                    Title = "Fish & Chips Stand"
                }
            };

            context.Services.AddRange(services);
            context.SaveChanges();
        }

        private void SeedClubs(HamerimDbContext context)
        {
            ICollection<Club> clubs = new List<Club>()
            {
                new Club()
                {
                    Id = 1,
                    Cost = 500,
                    Name = "The Forum"
                },
                new Club()
                {
                    Id = 2,
                    Cost = 700,
                    Name = "Hangar 11"
                }
            };

            context.Clubs.AddRange(clubs);
            context.SaveChanges();
        }

        private void SeedClubAddresses(HamerimDbContext context)
        {
            ICollection<ClubAddress> clubAddresses = new List<ClubAddress>()
            {
                new ClubAddress()
                {
                    ClubId = 1,
                    City = "Beer-Sheva",
                    Street = "Kiryat Yehudit IZ",
                    HouseNumber = 0
                },
                new ClubAddress()
                {
                    ClubId = 2,
                    City = "Tel-Aviv",
                    Street = "Yordei Hasira",
                    HouseNumber = 1
                }
            };

            context.ClubAddresses.AddRange(clubAddresses);
            context.SaveChanges();
        }

        private void SeedOrders(HamerimDbContext context)
        {
            IList<Service> services = context.Services.ToList();
            IList<Club> clubs = context.Clubs.ToList();
            ICollection<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Club = clubs.First(club => club.Name=="The Forum"),
                    ClientName = "Amir Yadid",
                    ClientPhone = "054-6494205",
                    Date = new DateTime(2020, 10, 23),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="Light Drinks Fridge"),
                        services.First(service => service.Title=="Fish & Chips Stand")
                    }
                },
                new Order()
                {
                    Id = 2,
                    Club = clubs.First(club => club.Name=="Hangar 11"),
                    ClientName = "Ofir Tsur",
                    ClientPhone = "054-9720633",
                    Date = new DateTime(2021, 8, 8),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="Alcohol Bar")
                    }
                },
                new Order()
                {
                    Id = 3,
                    Club = clubs.First(club => club.Name=="Hangar 11"),
                    ClientName = "Zohar Uzieli",
                    ClientPhone = "052-2568842",
                    Date = new DateTime(2020, 8, 13)
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}