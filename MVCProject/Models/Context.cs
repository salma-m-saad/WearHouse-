using Microsoft.EntityFrameworkCore;

namespace MVCProject.Models
{
    public class Context:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-D39S0SA;Initial catalog=ClothingShop;Integrated security=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
