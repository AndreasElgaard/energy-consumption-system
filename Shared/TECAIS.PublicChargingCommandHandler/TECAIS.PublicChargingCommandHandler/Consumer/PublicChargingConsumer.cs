using MassTransit;
using ModelContracts;
using SagaContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.PublicChargingCommandHandler.Services;

namespace TECAIS.PublicChargingCommandHandler.Consumer
{
    class PublicChargingConsumer : IConsumer<PublicChargingCommand>
    {
        private readonly IPublicChargingService _publicChargingService;
        public PublicChargingConsumer(IPublicChargingService service)
        {
            _publicChargingService = service;

        }
        public async Task Consume(ConsumeContext<PublicChargingCommand> context)
        {
            try
            {
                Console.WriteLine("Consuming message:");
                Console.WriteLine(context.Message);

                var repsonse = await _publicChargingService.GetPrice();

                await context.Publish<HeatSubmissionCharged>(new
                {
                    PublicCharging = repsonse,
                    Id = context.CorrelationId
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Consumer error: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
