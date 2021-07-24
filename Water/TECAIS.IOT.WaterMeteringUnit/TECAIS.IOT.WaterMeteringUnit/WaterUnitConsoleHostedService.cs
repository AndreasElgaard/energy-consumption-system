using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.IOT.WaterMeteringUnit.Models;
using TECAIS.IOT.WaterMeteringUnit.Services;

namespace TECAIS.IOT.WaterMeteringUnit
{
    public class WaterUnitConsoleHostedService : IHostedService
    {
        private readonly IWaterSubmissionService _waterSubmissionService;
        private int? _exitCode;

        public WaterUnitConsoleHostedService(IWaterSubmissionService waterSubmissionService)
        {
            _waterSubmissionService = waterSubmissionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting");
            var measurement = 10;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Posting");
                measurement = measurement + 1;

                await _waterSubmissionService.PostHeatSubmission(new WaterSubmission
                {
                    Address = "Krusaavej29",
                    TimeOfMeasurement = DateTime.Now,
                    WaterComsumption = measurement
                });

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //implement stop function 
            Console.WriteLine("Stopping");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
