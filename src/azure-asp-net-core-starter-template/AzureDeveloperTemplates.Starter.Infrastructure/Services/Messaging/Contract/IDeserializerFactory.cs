namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract
{
    public interface IDeserializerFactory<out T>
    {
        T Deserialize(string contentType, byte[] body);
    }
}
