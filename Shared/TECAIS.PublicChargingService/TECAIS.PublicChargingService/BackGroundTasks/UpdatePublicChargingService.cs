using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.PublicChargingService.Services;

namespace TECAIS.PublicChargingService.BackGroundTasks
{
    public class UpdatePublicChargingService : BackgroundService
    {
        public UpdatePublicChargingService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double charging = 20;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (charging < 20) {
                    charging += 5;
                } 
                else
                {
                    charging -= 5;
                }
                    

                
                Console.WriteLine($"hello price background service: {charging}");

                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                using (var scope = Services.CreateScope())
                {
                    var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

                    cacheService.UpdateCachedPrice(charging);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
