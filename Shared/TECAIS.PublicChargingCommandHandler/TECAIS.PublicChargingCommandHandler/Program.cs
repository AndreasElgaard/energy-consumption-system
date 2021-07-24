using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.PublicChargingCommandHandler.Consumer;
using TECAIS.PublicChargingCommandHandler.Services;

namespace TECAIS.PublicChargingCommandHandler
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
                })
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<PublicChargingConsumer>(typeof(PublicChargingConsumerDefinition));


                        cfg.UsingRabbitMq(ConfigureBus);
                    });


                    services.AddHostedService<PublicChargingConsoleHostedService>();
                    services.AddHttpClient<IPublicChargingService, PublicChargingService>();

                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddSerilog(dispose: true);
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                });



            if (isService)
                await builder.UseWindowsService().Build().RunAsync();
            else
                await builder.RunConsoleAsync();


            Log.CloseAndFlush();
        }



        static void ConfigureBus(IBusRegistrationContext busRegistrationContext, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.Host("34.88.94.207", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            configurator.ConfigureEndpoints(busRegistrationContext);
        }
    }
}
