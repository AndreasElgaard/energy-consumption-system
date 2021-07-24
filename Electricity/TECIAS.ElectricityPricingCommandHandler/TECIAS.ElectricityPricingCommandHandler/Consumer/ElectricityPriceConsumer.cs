using MassTransit;
using ModelContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECIAS.ElectricityPricingCommandHandler.Services;

namespace TECIAS.ElectricityPricingCommandHandler.Consumer
{
    class ElectricityPriceConsumer : IConsumer<ElectricityPriceCommand>
    {
        private readonly IElectricityPricingService _electricityPricingService;
        private readonly ILogger _logger;
        public ElectricityPriceConsumer(IElectricityPricingService service, ILogger logger)
        {
            _logger = logger;
            _electricityPricingService = service;

        }
        public async Task Consume(ConsumeContext<ElectricityPriceCommand> context)
        {
            try
            {
                _logger.Information("Consuming message: {@message}", context.Message);

                var repsonse = await _electricityPricingService.GetPrice();

            }
            catch (Exception e)
            {
                _logger.Error("Consumer error: ", e);
            }
        }
    }
}
