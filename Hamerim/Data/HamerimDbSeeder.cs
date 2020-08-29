using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Hamerim.Models;
using Microsoft.Ajax.Utilities;

namespace Hamerim.Data
{
    public class HamerimDbSeeder
    {
        static HamerimDbSeeder()
        {
            Seeder = new HamerimDbSeeder();
        }

        public static HamerimDbSeeder Seeder { get; }

        public void Seed(HamerimDbContext context)
        {
            SeedServiceCategories(context);
            SeedServices(context);
            SeedClubs(context);
            SeedClubAddresses(context);
            SeedOrders(context);
            SeedUsers(context);
        }

        private void SeedServiceCategories(HamerimDbContext context)
        {
            ServiceCategory[] serviceCategories = {
                new ServiceCategory()
                {
                    Id = 1,
                    Title = "Drinks"
                },
                new ServiceCategory()
                {
                    Id = 2,
                    Title = "Food"
                },
                new ServiceCategory()
                {
                    Id = 3,
                    Title = "Additional Services"
                }
            };

            context.ServiceCategories.AddOrUpdate(serviceCategories);
            context.SaveChanges();
        }

        private void SeedServices(HamerimDbContext context)
        {
            IList<ServiceCategory> categories = context.ServiceCategories.ToList();
            Service[] services = {
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
                    Category = categories.First(category => category.Title=="Drinks"),
                    Cost = 170,
                    Title = "Premium Alcohol Bar"
                },
                new Service()
                {
                    Id = 4,
                    Category = categories.First(category => category.Title=="Food"),
                    Cost = 200,
                    Title = "Fish & Chips Stand"
                },
                new Service()
                {
                    Id = 5,
                    Category = categories.First(category => category.Title=="Food"),
                    Cost = 230,
                    Title = "Sushi Stand"
                }
            };

            context.Services.AddOrUpdate(services);
            context.SaveChanges();
        }

        private void SeedClubs(HamerimDbContext context)
        {
            Club[] clubs = {
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

            context.Clubs.AddOrUpdate(clubs);
            context.SaveChanges();
        }

        private void SeedClubAddresses(HamerimDbContext context)
        {
            ClubAddress[] clubAddresses = {
                new ClubAddress()
                {
                    ClubId = 1,
                    City = "Beer-Sheva",
                    Street = "Kiryat Yehudit",
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

            context.ClubAddresses.AddOrUpdate(clubAddresses);
            context.SaveChanges();
        }

        private void SeedOrders(HamerimDbContext context)
        {
            IList<Service> services = context.Services.ToList();
            IList<Club> clubs = context.Clubs.ToList();
            Order[] orders = {
                new Order()
                {
                    Id = 100000,
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
                    Id = 100001,
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
                    Id = 100002,
                    Club = clubs.First(club => club.Name=="Hangar 11"),
                    ClientName = "Zohar Uzieli",
                    ClientPhone = "052-2568842",
                    Date = new DateTime(2020, 8, 13)
                },
                new Order()
                {
                    Id = 100003,
                    Club = clubs.First(club => club.Name=="Hangar 11"),
                    ClientName = "Brad Pitt",
                    ClientPhone = "052-6667866",
                    Date = new DateTime(2020, 8, 29),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="Light Drinks Fridge"),
                        services.First(service => service.Title=="Alcohol Bar")
                    }
                },
                new Order()
                {
                    Id = 100004,
                    Club = clubs.First(club => club.Name=="The Forum"),
                    ClientName = "George Costanza",
                    ClientPhone = "052-4476623",
                    Date = new DateTime(2020, 8, 13),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="Light Drinks Fridge")
                    }
                }
            };

            context.Orders.AddOrUpdate(orders);
            context.SaveChanges();
        }

        private void SeedUsers(HamerimDbContext context)
        {
            User[] users =
            {
                new User()
                {
                    Id = 1,
                    Username = "Admin",
                    Password = "123456",
                    IsAdmin = true
                }
            };

            context.Users.AddOrUpdate(users);
            context.SaveChanges();
        }
    }
}