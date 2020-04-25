using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class EventsServiceCollectionExtensions
    {
        public static IServiceCollection AddEventsService(this IServiceCollection services)
        {
            services.TryAddSingleton<IEventsReceiverService, EventsReceiverService>();
            services.TryAddSingleton<IEventsSenderService, EventsSenderService>();
            services.TryAddSingleton<IReceivedEventsProcessor, ReceivedEventsProcessor>();
            services.TryAddSingleton<EventsBackgroundService>();
            return services;
        }
    }
}
