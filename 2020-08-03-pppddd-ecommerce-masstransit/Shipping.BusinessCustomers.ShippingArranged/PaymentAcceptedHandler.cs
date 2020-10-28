using Billing.Messages.Events;
using MassTransit;
using Sales.Messages.Events;
using System;
using System.Threading.Tasks;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public class PaymentAcceptedHandler : IConsumer<PaymentAccepted>
    {
        public async Task Consume(ConsumeContext<PaymentAccepted> context)
        {
            var message = context.Message;
            var address = ShippingDatabase.GetCustomerAddress(message.OrderId);
            var confirmation = ShippingProvider.ArrangeShippingFor(address, message.OrderId);

            if (confirmation.Status == ShippingStatus.Success)
            {
                await context.Publish<Messages.Events.ShippingArranged>(new
                {
                    OrderId = message.OrderId
                });

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(
                    "\n--->> Shipping BC arranged shipping for Order: {0}\n",
                    message.OrderId, address
                );
                Console.ResetColor();
            }
            else
            {
                // .. notify failed shipping instead
            }
        }
    }
}
