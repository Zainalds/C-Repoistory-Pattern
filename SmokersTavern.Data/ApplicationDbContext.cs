using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using SmokersTavern.Data.Models;

//Zain
namespace SmokersTavern.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }      
        public DbSet<CategoryArchive> CategoryArchives { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<StaffArchive> StaffArchives { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<StockTransactions> StockTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<SupplierArchive> SupplierArchives { get; set; }
        public DbSet<Sales> Sale { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
