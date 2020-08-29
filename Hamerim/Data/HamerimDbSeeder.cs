using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
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
            SeedOrders(context);
            SeedUsers(context);
        }

        private void SeedServiceCategories(HamerimDbContext context)
        {
            ServiceCategory[] serviceCategories = {
                new ServiceCategory()
                {
                    Id = 1,
                    Title = "שתייה"
                },
                new ServiceCategory()
                {
                    Id = 2,
                    Title = "אוכל"
                },
                new ServiceCategory()
                {
                    Id = 3,
                    Title = "שירותים נוספים"
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
                    Category = categories.First(category => category.Title=="שתייה"),
                    Cost = 50,
                    Title = "מקרר שתייה קלה"
                },
                new Service()
                {
                    Id = 2,
                    Category = categories.First(category => category.Title=="שתייה"),
                    Cost = 100,
                    Title = "בר אלכוהול"
                },
                new Service()
                {
                    Id = 3,
                    Category = categories.First(category => category.Title=="שתייה"),
                    Cost = 170,
                    Title = "בר אלכוהול יוקרתי"
                },
                new Service()
                {
                    Id = 4,
                    Category = categories.First(category => category.Title=="אוכל"),
                    Cost = 200,
                    Title = "דוכן שווארמה"
                },
                new Service()
                {
                    Id = 5,
                    Category = categories.First(category => category.Title=="אוכל"),
                    Cost = 230,
                    Title = "בופה סושי"
                },
                new Service()
                {
                    Id = 6,
                    Category = categories.First(category => category.Title=="שירותים נוספים"),
                    Cost = 600,
                    Title = "בריכה מתנפחת"
                },
                new Service()
                {
                    Id = 7,
                    Category = categories.First(category => category.Title=="שירותים נוספים"),
                    Cost = 1100,
                    Title = "דיג'יי לכל הערב"
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
                    Name = "הפורום",
                    Address = new ClubAddress()
                    {
                        ClubId = 1,
                        City = "Beer-Sheva",
                        Street = "Kiryat Yehudit",
                        HouseNumber = 0
                    }
                },
                new Club()
                {
                    Id = 2,
                    Cost = 700,
                    Name = "האנגר 11",
                    Address = new ClubAddress()
                    {
                        ClubId = 2,
                        City = "Tel-Aviv",
                        Street = "Yordei Hasira",
                        HouseNumber = 1
                    }
                },
                new Club()
                {
                    Id = 3,
                    Cost = 1100,
                    Name = "הוואנה מיוזיק קלאב",
                    Address = new ClubAddress()
                    {
                        ClubId = 3,
                        City = "Tel-Aviv",
                        Street = "Yigal Alon",
                        HouseNumber = 126
                    }
                },
                new Club()
                {
                    Id = 4,
                    Cost = 1000,
                    Name = "דוקטור גונזו",
                    Address = new ClubAddress()
                    {
                        ClubId = 4,
                        City = "Kfar-Saba",
                        Street = "Hayozma",
                        HouseNumber = 3
                    }
                },
                new Club()
                {
                    Id = 5,
                    Cost = 1500,
                    Name = "הבלוק",
                    Address = new ClubAddress()
                    {
                        ClubId = 5,
                        City = "Tel-Aviv",
                        Street = "Shalma Rd",
                        HouseNumber = 157
                    }
                },
                new Club()
                {
                    Id = 6,
                    Cost = 800,
                    Name = "ברקפסט קלאב",
                    Address = new ClubAddress()
                    {
                        ClubId = 6,
                        City = "Tel-Aviv",
                        Street = "Rothschild Blvd",
                        HouseNumber = 6
                    }
                }
            };

            context.Clubs.AddOrUpdate(clubs);
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
                    Club = clubs.First(club => club.Name=="הפורום"),
                    ClientName = "אמיר ידיד",
                    ClientPhone = "054-6494205",
                    Date = new DateTime(2020, 10, 23),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="מקרר שתייה קלה"),
                        services.First(service => service.Title=="דוכן שווארמה")
                    }
                },
                new Order()
                {
                    Id = 100001,
                    Club = clubs.First(club => club.Name=="האנגר 11"),
                    ClientName = "אופיר צור",
                    ClientPhone = "054-9720633",
                    Date = new DateTime(2021, 8, 8),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="בר אלכוהול")
                    }
                },
                new Order()
                {
                    Id = 100002,
                    Club = clubs.First(club => club.Name=="האנגר 11"),
                    ClientName = "זהר עוזיאלי",
                    ClientPhone = "052-2568842",
                    Date = new DateTime(2020, 8, 13)
                },
                new Order()
                {
                    Id = 100003,
                    Club = clubs.First(club => club.Name=="האנגר 11"),
                    ClientName = "בראד פיט",
                    ClientPhone = "052-6667866",
                    Date = new DateTime(2020, 8, 29),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="מקרר שתייה קלה"),
                        services.First(service => service.Title=="בר אלכוהול")
                    }
                },
                new Order()
                {
                    Id = 100004,
                    Club = clubs.First(club => club.Name=="הפורום"),
                    ClientName = "ג'ורג' קוסטנזה",
                    ClientPhone = "052-4476623",
                    Date = new DateTime(2020, 8, 13),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="מקרר שתייה קלה")
                    }
                },
                new Order()
                {
                    Id = 100005,
                    Club = clubs.First(club => club.Name=="הבלוק"),
                    ClientName = "יוחנן שפמוביץ'",
                    ClientPhone = "052-7325564",
                    Date = new DateTime(2020,9,11),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="מקרר שתייה קלה"),
                        services.First(service => service.Title=="בר אלכוהול יוקרתי"),
                        services.First(service => service.Title=="בופה סושי"),
                    }
                },
                new Order()
                {
                    Id = 100006,
                    Club = clubs.First(club => club.Name=="הוואנה מיוזיק קלאב"),
                    ClientName = "ביבי נתניהו",
                    ClientPhone = "052-3349511",
                    Date = new DateTime(2020,10,20),
                    ServicesInOrder = new HashSet<Service>()
                    {
                        services.First(service => service.Title=="בריכה מתנפחת"),
                        services.First(service => service.Title=="בר אלכוהול יוקרתי"),
                        services.First(service => service.Title=="דיג'יי לכל הערב"),
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