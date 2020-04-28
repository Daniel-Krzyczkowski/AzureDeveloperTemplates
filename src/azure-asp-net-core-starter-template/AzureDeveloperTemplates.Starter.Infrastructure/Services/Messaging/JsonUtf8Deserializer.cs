using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract;
using Newtonsoft.Json;
using System.Text;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public sealed class JsonUtf8Deserializer<T> : IDeserializer<T>
    {
        public string Charset => "utf-8";

        public T Deserialize(byte[] body) => JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(body));
    }

}
