namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IStorageServiceConfiguration
    {
        string ContainerName { get; set; }
        string BlobStorageUrl { get; set; }
    }
}
