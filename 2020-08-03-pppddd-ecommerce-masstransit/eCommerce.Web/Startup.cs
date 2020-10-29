using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sales.Messages.Commands;
using Sales.Messages.Requests;

namespace eCommerce.Web
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
            var appConfig = Configuration.GetSection("AppConfig").Get<AppConfig>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(appConfig.RabbitMq.HostAddress, appConfig.RabbitMq.VirtualHost, h =>
                    {
                        h.Username(appConfig.RabbitMq.Username);
                        h.Password(appConfig.RabbitMq.Password);
                    });
				});

                x.AddRequestClient<PlaceOrder>();

                // NOTE: this is needed if you use sendEndpointProvider.Send() https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
                // EndpointConvention.Map<PlaceOrder>(new Uri("queue:place-order-handler")); 
            });
            services.AddMassTransitHostedService();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            var timestamp = InVar.Timestamp;

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
