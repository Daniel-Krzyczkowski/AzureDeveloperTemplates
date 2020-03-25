using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services
{
    public sealed class JsonUtf8Deserializer<T> : IDeserializer<T>
    {
        public string Charset => "utf-8";

        public T Deserialize(byte[] body) => JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
    }

}
