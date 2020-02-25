using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Contracts
{
    public interface IDeserializerFactory<out T>
    {
        T Deserialize(string contentType, byte[] body);
    }
}
