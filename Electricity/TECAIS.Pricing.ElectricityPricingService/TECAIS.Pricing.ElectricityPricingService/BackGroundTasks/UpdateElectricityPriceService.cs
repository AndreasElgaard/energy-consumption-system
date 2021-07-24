using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.Pricing.ElectricityPricingService.Services;

namespace TECAIS.Pricing.ElectricityPricingService.BackGroundTasks
{
    public class UpdateElectricityPriceService : BackgroundService
    {
        public UpdateElectricityPriceService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double electricityPrice = 2;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (electricityPrice < 2.25)
                {
                    electricityPrice += 0.15;
                }
                else
                {
                    electricityPrice -= 0.15;
                }



                Console.WriteLine($"hello price background service: {electricityPrice}");

                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                using (var scope = Services.CreateScope())
                {
                    var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

                    cacheService.UpdateCachedPrice(electricityPrice);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
