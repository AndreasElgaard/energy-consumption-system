using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Saga;

namespace TECAIS.WaterSubmissionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WaterSubmissionController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint; 

        private readonly ILogger<WaterSubmissionController> _logger;

        public WaterSubmissionController(ILogger<WaterSubmissionController> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        //POST - Creates a new heat submission
        [HttpPost]
        public async Task<IActionResult> CreateWaterSubmission(WaterSubmission waterSubmission)
        {
            await _endpoint.Publish<WaterSubmission>(new
            {
                Address = waterSubmission.Address,
                TimeOfMeasurement = waterSubmission.TimeOfMeasurement,
                WaterConsumption = waterSubmission.WaterConsumption
            });

            return Ok();
        }
    }
}
