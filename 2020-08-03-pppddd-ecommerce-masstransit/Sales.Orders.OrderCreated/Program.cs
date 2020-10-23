using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sales.Orders.OrderCreated.Application;

namespace Sales.Orders.OrderCreated
{
    class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("-- SALES --");

			var isService = !(Debugger.IsAttached || args.Contains("--console"));

			// The “generic” Host and HostBuilder are components of a new feature set coming with the release 
			// of .NET Core 2.1.A use case of them is to simplify the creation of console based services by 
			// providing a pattern for adding cross-cutting concerns such as dependency injection, configuration
			// and logging. - https://www.stevejgordon.co.uk/using-generic-host-in-dotnet-core-console-based-microservices
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
					services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
					services.AddMassTransit(cfg =>
					{
						// AddBus has been superseded by UsingRabbitMQ (and other transport-specific extension methods) - https://masstransit-project.com/getting-started/upgrade-v6.html#version-7
						cfg.UsingRabbitMq(ConfigureBus);

						cfg.AddConsumer<PlaceOrderHandler>();
					});

					services.AddHostedService<MassTransitConsoleHostedService>();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
				});

			if (isService)
				await builder.UseWindowsService().Build().RunAsync();
			else
				await builder.RunConsoleAsync();
		}

		// NOTE_JBOY: this function is different from that in the tutorial;
		// this was taken from the completed project available on Github - https://github.com/MassTransit/Sample-Twitch
		static void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
		{
			configurator.ConfigureEndpoints(context);
		}

		//public static async Task Main()
  //      {
  //          Console.WriteLine("-- SALES --");

  //          var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
  //          {
  //              cfg.ReceiveEndpoint("place-order-handler", e =>
  //              {
  //                  e.Consumer<PlaceOrderHandler>();
  //              });
  //          });
  //          var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
  //          await busControl.StartAsync(source.Token);

  //          try
  //          {
  //              Console.WriteLine("Press enter to exit");
  //              await Task.Run(() => Console.ReadLine());
  //          }
  //          finally
  //          {
  //              await busControl.StopAsync();
  //          }
  //      }
    }
}
