﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shipping.BusinessCustomers.ShippingArranged
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("-- SHIPPING --");

			var isService = !(Debugger.IsAttached || args.Contains("--console"));

			// The “generic” Host and HostBuilder are components of a new feature set coming with the release 
			// of .NET Core 2.1. A use case of them is to simplify the creation of console based services by 
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

						//cfg.AddConsumer<OrderCreatedHandler>();
						//cfg.AddConsumer<PaymentAcceptedHandler>();
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

		static void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
		{
			//configurator.ConfigureEndpoints(context);
			configurator.ReceiveEndpoint("Shipping.BusinessCustomers.ShippingArranged", cfg =>
			{
				cfg.Consumer<OrderCreatedHandler>();
				cfg.Consumer<PaymentAcceptedHandler>();
			});
		}

		//public static async Task Main()
		//{
		//	Console.WriteLine("-- SALES --");

		//	var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
		//	{
		//		cfg.ReceiveEndpoint("place-order-handler", e =>
		//		{
		//			e.Consumer<PlaceOrderHandler>();
		//		});
		//	});
		//	var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
		//	await busControl.StartAsync(source.Token);

		//	try
		//	{
		//		Console.WriteLine("Press enter to exit");
		//		await Task.Run(() => Console.ReadLine());
		//	}
		//	finally
		//	{
		//		await busControl.StopAsync();
		//	}
		//}
	}

	// When you're using ASP.NET, it has a class already built for you to run as a hosted service
	// But because we are creating a console hosted service, we have to create a simple hosted service.
	// All it does, because it is a IHostedService, is it gets added to the container. The .NET core generic
	//      host will actually start this up for us and call StartAsync() which we'll use to start out bus,
	//      passing it the cancellation token if they decide to give up on us. - https://www.youtube.com/watch?v=97PXJIrGnes
	class MassTransitConsoleHostedService : IHostedService
	{
		readonly IBusControl _bus;

		public MassTransitConsoleHostedService(IBusControl bus)
		{
			_bus = bus;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _bus.StopAsync(cancellationToken);
		}
	}

}