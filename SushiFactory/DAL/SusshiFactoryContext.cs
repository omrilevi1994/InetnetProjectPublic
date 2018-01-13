using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SushiFactory.Models;

namespace SushiFactory.DAL
{
    public class SusshiFactoryContext : DbContext
    {
        public SusshiFactoryContext() : base("SusshiFactoryContext")
	    {
	    }

        public DbSet<Product> Menu { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Tables name in RABIM 
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}