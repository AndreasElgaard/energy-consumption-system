using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.Pricing.HeatPricingService.Services;

namespace TECAIS.Pricing.HeatPricingService.BackGroundTasks
{
    public class UpdateHeatPriceService : BackgroundService
    {
        public UpdateHeatPriceService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double heatPrice = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (heatPrice < 1) {
                    heatPrice += 0.2;
                } 
                else
                {
                    heatPrice -= 0.2;
                }
                    

                
                Console.WriteLine($"hello price background service: {heatPrice}");

                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                using (var scope = Services.CreateScope())
                {
                    var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

                    cacheService.UpdateCachedPrice(heatPrice);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
