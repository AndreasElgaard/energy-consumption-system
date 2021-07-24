using MassTransit;
using ModelContracts;
using SagaContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.AccountCommandHandler.Services;

namespace TECAIS.AccountCommandHandler.Consumer
{
    class AccountingConsumer : IConsumer<AccountingCommand>
    {
        private readonly IAccountingService _accountingService;
        public AccountingConsumer(IAccountingService service)
        {

            _accountingService = service;

        }
        public async Task Consume(ConsumeContext<AccountingCommand> context)
        {
            try
            {
                Console.WriteLine("Consuming message: ");
                Console.WriteLine(context.Message);


                var repsonse = await _accountingService.UpdateAccount(context.Message);

                await context.Publish<HeatSubmissionAccounted>(new
                {
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
