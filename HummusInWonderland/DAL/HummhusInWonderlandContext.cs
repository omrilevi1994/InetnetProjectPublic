using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HummusInWonderland.Models;

namespace HummusInWonderland.DAL
{
    public class HummhusInWonderlandContext : DbContext
    {
        public HummhusInWonderlandContext() : base("HummhusInWonderlandContext")
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