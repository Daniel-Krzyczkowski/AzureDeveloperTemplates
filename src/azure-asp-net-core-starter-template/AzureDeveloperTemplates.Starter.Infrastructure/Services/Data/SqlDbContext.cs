using AzureDeveloperTemplates.Starter.Core.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
                                                                : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Samsung TV"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "MacBook"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Surface Laptop"
                    }
                );
        }
    }
}
