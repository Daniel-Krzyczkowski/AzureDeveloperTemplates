using AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                                                                        : base(options)
        {
        }

        public DbSet<SampleEntity> SampleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SampleEntity>().HasData(new SampleEntity
            {
                Id = Guid.NewGuid()
            });
        }
    }
}
