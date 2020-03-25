using AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.Infrastructure.Data
{
    public class SampleRepository : EfRepository<SampleEntity>
    {
        public SampleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
