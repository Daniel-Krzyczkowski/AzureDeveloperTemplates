namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract
{
    public interface IDeserializer<out T>
    {
        T Deserialize(byte[] body);
        string Charset { get; }
    }
}
