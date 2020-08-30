using System.Configuration;
using System.Data.Entity;
using Hamerim.Models;

namespace Hamerim.Data
{
    public class HamerimDbContext : DbContext
    {
        public HamerimDbContext() : base("HamerimDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HamerimDbContext, Migrations.Configuration>());
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories  { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubAddress> ClubAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}