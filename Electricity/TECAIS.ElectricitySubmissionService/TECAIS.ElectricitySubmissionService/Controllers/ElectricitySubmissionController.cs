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

namespace TECAIS.ElectricitySubmissionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectricitySubmissionController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint;

        private readonly ILogger<ElectricitySubmissionController> _logger;

        public ElectricitySubmissionController(ILogger<ElectricitySubmissionController> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        //POST - Creates a new heat submission
        [HttpPost]
        public async Task<IActionResult> CreateWaterSubmission(ElectricitySubmission electricitySubmission)
        {
            //await _endpoint.Publish<ElectricitySubmission>(new
            //{
            //    Address = electricitySubmission.Address,
            //    TimeOfMeasurement = electricitySubmission.TimeOfMeasurement,
            //    WaterConsumption = electricitySubmission.ElectricityConsumption
            //});

            return Ok();
        }

        //POST - Creates a new heat submission
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _endpoint.Publish<ElectricitySubmission>(new
            {
                Address = "Hej",
                TimeOfMeasurement = DateTime.Now,
                WaterConsumption = 44
            });
            return Ok("Hej med dig");
        }
    }
}
