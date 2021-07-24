using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Services;

namespace TECAIS.IOT.HeatMeteringUnit
{
    public class UnitConsoleHostedService : IHostedService
    {
        private readonly IHeatSubmissionService _heatSubmissionService;
        private int? _exitCode;

        public UnitConsoleHostedService(IHeatSubmissionService heatSubmissionService)
        {
            _heatSubmissionService = heatSubmissionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting");
            Random r = new Random();
            var measurement = 10;

            var count = 1;

            while (!cancellationToken.IsCancellationRequested)
            {

                Console.WriteLine("Posting");
                measurement = measurement + 1;

                await _heatSubmissionService.PostHeatSubmission(new Models.HeatSubmission
                {
                    Address = "Krusaavej29",
                    TimeOfMeasurement = DateTime.Now,
                    HeatComsumption = measurement
                });

                if (count == 5)
                {
                    Console.WriteLine("Posting status");

                    var randNum = r.Next(0, 10);
                    var status = "";
                    if (randNum >= 8)
                    {
                        status = "Failure";
                    }
                    else
                    {
                        status = "OK";
                    }

                    Console.WriteLine("Posting status");


                    await _heatSubmissionService.PostStatusSubmission(new Models.StatusSubmission
                    {
                        Address = "Krusaavej29",
                        TimeOfStatus = DateTime.Now,
                        Status = status
                    });
                    count = 1;
                }
                else
                {
                    count++;
                }

                await Task.Delay(TimeSpan.FromSeconds(15));
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
