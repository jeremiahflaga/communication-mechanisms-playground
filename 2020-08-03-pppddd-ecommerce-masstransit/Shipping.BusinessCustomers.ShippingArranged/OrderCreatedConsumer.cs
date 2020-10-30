using MassTransit;
using Sales.Messages.Events;
using System;
using System.Threading.Tasks;

namespace Shipping.BusinessCustomers.ShippingArranged
{
	class OrderCreatedConsumer : IConsumer<OrderCreated_V2>
	{
		public async Task Consume(ConsumeContext<OrderCreated_V2> context)
		{
			var message = context.Message;

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(
				"\n--->> Shipping BC storing: Order: {0} User: {1} Shipping Type: {2}\n",
				message.OrderId, message.UserId, message.ShippingTypeId, message.AddressId
			);
			Console.ResetColor();

			var order = new ShippingOrderDbModel
			{
				UserId = message.UserId,
				OrderId = message.OrderId,
				AddressId = message.AddressId,
				ShippingTypeId = message.ShippingTypeId
			};
			ShippingDatabase.AddOrderDetails(order);
		}
	}

}
