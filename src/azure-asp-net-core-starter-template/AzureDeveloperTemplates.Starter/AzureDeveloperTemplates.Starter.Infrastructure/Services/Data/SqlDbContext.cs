using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using Microsoft.EntityFrameworkCore;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
                                                                : base(options)
        {
        }

        public DbSet<IEntity> SampleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
