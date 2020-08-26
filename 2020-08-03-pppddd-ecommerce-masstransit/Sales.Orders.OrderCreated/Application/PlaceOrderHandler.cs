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

            Console.WriteLine(
                @"Created order #{3} : Products:{0} with shipping: {1} made by user: {2}",
                String.Join(",", message.ProductIds), message.ShippingTypeId, message.UserId, orderId
            );

            // sending a V2 message now
            var orderCreatedEvent = new Sales.Messages.Events.OrderCreated_V2
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now,
                Amount = CalculateCostOf(message.ProductIds),
                /*
                 * add a new field to the form and the PlaceOrder command
                 * if you don't want to hard-code the value
                 */
                AddressId = "AddressID123"
            };

            await context.Publish(orderCreatedEvent);
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
