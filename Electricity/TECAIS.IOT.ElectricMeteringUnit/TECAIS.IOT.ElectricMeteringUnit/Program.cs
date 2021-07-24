using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Control;
using TECAIS.IOT.ElectricMeteringUnit.Services;

namespace TECAIS.IOT.ElectricMeteringUnit
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);

                }).ConfigureServices((hostingContext, services) =>
                {
                    services.AddHostedService<HealthBackgroundTask>();
                    services.AddHttpClient<IStatusSubmissionService, StatusSubmissionService>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) =>
                        {
                            return true;
                        }
                    });


                    //Add dependencies to service collection
                    services.AddHostedService<UnitConsoleHostedService>();
                    services.AddHttpClient<IElectricSubmissionService, ElectricSubmissionService>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) =>
                        {
                            return true;
                        }
                    });

                }).ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddSerilog(dispose: true);
                });


            if (isService)
                await builder.UseWindowsService().Build().RunAsync();
            else
                await builder.RunConsoleAsync();

            Log.CloseAndFlush();
        }
    }
}
