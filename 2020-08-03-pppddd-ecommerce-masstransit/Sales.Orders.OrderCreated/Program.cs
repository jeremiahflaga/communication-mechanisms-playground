using MassTransit;
using Sales.Orders.OrderCreated.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sales.Orders.OrderCreated
{
    class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("-- SALES --");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("place-order-handler", e =>
                {
                    e.Consumer<PlaceOrderHandler>();
                });
            });
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);

            try
            {
                Console.WriteLine("Press enter to exit");
                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
