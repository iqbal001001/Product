using Product.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Product.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public ProductContext() : base()
        {
        }

        public DbSet<ProductInfo> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductInfo>()
           .Property(b => b.Name).HasMaxLength(200);
        }
    }
}