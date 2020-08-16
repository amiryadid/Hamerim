using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hamerim.Models;

namespace Hamerim.Data
{
    public class HamerimDbContext : DbContext
    {
        public HamerimDbContext() : base("HamerimDB")
        {
            bool shouldInitialize = bool.Parse(ConfigurationManager.AppSettings["InitializeDatabase"]);
            HamerimDbInitializer initializer = shouldInitialize ? new HamerimDbInitializer() : null;

            Database.SetInitializer<HamerimDbContext>(initializer);
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories  { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubAddress> ClubAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}