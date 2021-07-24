using MassTransit;
using ModelContracts;
using SagaContracts;
using System;
using System.Threading.Tasks;
using TECAIS.HeatPricingCommandHandler.Services;

namespace TECAIS.HeatPricingCommandHandler.Consumer
{
    public class HeatPriceConsumer : IConsumer<HeatPriceCommand>
    {
        private readonly IHeatPricingService _heatPricingService;
       // private readonly ILogger _logger;
        public HeatPriceConsumer(IHeatPricingService service)
        {
            //_logger = logger;
            _heatPricingService = service;
            
        }
        public async Task Consume(ConsumeContext<HeatPriceCommand> context)
        {
            try
            {
                Console.WriteLine("Consuming message: ");
                Console.WriteLine(context.Message);

                var repsonse = await _heatPricingService.GetPrice();

                await context.Publish<HeatSubmissionPriced>(new
                {
                    Price = repsonse,
                    Id = context.CorrelationId
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Consumer error: ");
                Console.WriteLine(e);
            }
        }
    }
}
