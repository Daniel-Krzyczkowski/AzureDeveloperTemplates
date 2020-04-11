using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbDataServiceFactory : IDataServiceFactory
    {
        private readonly IDataService<IEntity> _sqlDbDataService;

        public SqlDbDataServiceFactory(IDataService<IEntity> sqlDbDataService)
        {
            _sqlDbDataService = sqlDbDataService;
        }

        public IDataService<IEntity> Create()
        {
            return _sqlDbDataService;
        }
    }
}
