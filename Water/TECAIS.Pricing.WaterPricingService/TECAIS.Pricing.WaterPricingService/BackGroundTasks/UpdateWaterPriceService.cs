using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.Pricing.WaterPricingService.Services;

namespace TECAIS.Pricing.WaterPricingService.BackGroundTasks
{
    public class UpdateWaterPriceService : BackgroundService
    {
        public UpdateWaterPriceService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double waterPrice = 17;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (waterPrice < 18)
                {
                    waterPrice += 2;
                }
                else
                {
                    waterPrice -= 2;
                }



                Console.WriteLine($"hello price background service: {waterPrice}");

                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                using (var scope = Services.CreateScope())
                {
                    var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

                    cacheService.UpdateCachedPrice(waterPrice);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
