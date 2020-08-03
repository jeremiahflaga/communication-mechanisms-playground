using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Billing.Payments.PaymentAccepted
{
    class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("-- BILLING --");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("order-created-handler", e =>
                {
                    e.Consumer<OrderCreatedHandler>();
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
