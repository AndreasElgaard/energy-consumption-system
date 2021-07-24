using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.AccountingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountingController : ControllerBase
    {       
        private readonly ILogger<AccountingController> _logger;

        public AccountingController(ILogger<AccountingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> PostAccountingModel(AccountingModel model)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Billing registered for address: " + model.Address);
            Console.WriteLine("Consumption type: " + model.ConsumptionType);
            Console.WriteLine("Cosumption: " + model.Consumption);
            var totalPrice = model.Consumption * model.Price + model.TaxPrice;
            Console.WriteLine("Price to pay: " + totalPrice);
            Console.WriteLine("-----------------------------------------------------");
            return Ok();
        }
    }
}
