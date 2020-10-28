using MassTransit;
using Sales.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Orders.OrderCreated.Application
{
    public class PlaceOrderHandler : IConsumer<PlaceOrder>
    {
        public async Task Consume(ConsumeContext<PlaceOrder> context)
        {
            var message = context.Message;
            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(
                "\n--->> Created order #{3} : Products:{0} with shipping: {1} made by user: {2}\n",
                String.Join(",", message.ProductIds), message.ShippingTypeId, message.UserId, orderId
            );
            Console.ResetColor();

            // sending a V2 message now

            await context.Publish<Messages.Events.OrderCreated_V2>(new
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTimeOffset.Now,
                Amount = CalculateCostOf(message.ProductIds),
                /*
                 * add a new field to the form and the PlaceOrder command
                 * if you don't want to hard-code the value
                 */
                AddressId = "AddressID123"
            });
        }

        private double CalculateCostOf(IEnumerable<string> productIds)
        {
            // database lookup, etc
            return 1000.00;
        }
    }

    // This could be any database technology. It can differ between Business Components
    public static class Database
    {
        private static int Id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = Id++;
            return nextOrderId.ToString();
        }
    }
}
