using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.Components.Consumers;
using MassTransitSample.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTransitSample
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// Container configuration has changed, and now uses the AddMediator method (instead of AddMassTransit) - https://masstransit-project.com/getting-started/upgrade-v6.html#version-7
			services.AddMediator(cfg =>
			{
				cfg.AddConsumer<SubmitOrderConsumer>();

				cfg.AddRequestClient<SubmitOrder>();
			});

			services.AddOpenApiDocument(cfg => cfg.PostProcess = d => d.Info.Title = "MassTransit Sample API site");

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseOpenApi(); // serve OpenAPI/Swagger documents
			app.UseSwaggerUi3(); // serve Swagger UI

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
