using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Services;

namespace TECAIS.IOT.ElectricMeteringUnit.Control
{
    public class HealthBackgroundTask : IHostedService
    {
        private readonly IStatusSubmissionService _statusSubmissionService;
        private int? _exitCode;

        public HealthBackgroundTask(IStatusSubmissionService statusSubmissionService)
        {
            _statusSubmissionService = statusSubmissionService;
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Random r = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
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


                await _statusSubmissionService.PostStatusSubmission(new Models.StatusSubmission
                {
                    Address = "Krusaavej29",
                    TimeOfStatus = DateTime.Now,
                    Status = status
                });

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("start status");

            //_timer = new Timer(ExecuteAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            ExecuteAsync(cancellationToken);

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop");

            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);

            return Task.CompletedTask;
        }
    }
}
