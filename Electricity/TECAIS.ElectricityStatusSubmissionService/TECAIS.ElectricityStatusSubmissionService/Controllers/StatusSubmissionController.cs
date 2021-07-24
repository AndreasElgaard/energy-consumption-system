using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.ElectricityStatusSubmissionService.Models;

namespace TECAIS.ElectricityStatusSubmissionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusSubmissionController : ControllerBase
    {
        private readonly ILogger<StatusSubmissionController> _logger;

        public StatusSubmissionController(ILogger<StatusSubmissionController> logger)
        {
            _logger = logger;
        }

        //POST - Creates a new heat submission
        [HttpPost]
        public async Task<IActionResult> CreateHeatSubmission(StatusSubmission statusSubmission)
        {
            _logger.LogInformation("Received from: " + statusSubmission.Address + " Status is: " + statusSubmission.Status + " at " + statusSubmission.TimeOfStatus);

            return Ok();
        }

        //POST - Creates a new heat submission
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hej");
        }
    }
}
