namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Contracts
{
    public interface IDeserializer<out T>
    {
        T Deserialize(byte[] body);
        string Charset { get; }
    }
}
