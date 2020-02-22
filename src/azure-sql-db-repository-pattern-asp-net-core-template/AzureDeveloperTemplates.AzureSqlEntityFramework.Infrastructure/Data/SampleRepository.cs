using AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.Infrastructure.Data
{
    public class SampleRepository : EfRepository<SampleEntity>
    {
        public SampleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
