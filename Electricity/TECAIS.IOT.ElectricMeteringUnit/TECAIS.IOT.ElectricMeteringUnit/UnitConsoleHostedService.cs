using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Services;

namespace TECAIS.IOT.ElectricMeteringUnit
{
    public class UnitConsoleHostedService : IHostedService
    {
        private readonly IElectricSubmissionService _electricSubmissionService;
        private int? _exitCode;

        public UnitConsoleHostedService(IElectricSubmissionService electricSubmissionService)
        {
            _electricSubmissionService = electricSubmissionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting");
            var measurement = 10;

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Posting");
                measurement = measurement + 1;

                await _electricSubmissionService.PostHeatSubmission(new Models.ElectricSubmission
                {
                    Address = "Krusaavej29",
                    TimeOfMeasurement = DateTime.Now,
                    ElectricityComsumption = measurement
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
