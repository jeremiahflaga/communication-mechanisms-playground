using Billing.Messages.Commands;
using MassTransit;
using Sales.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Payments.PaymentAccepted
{
    class OrderCreatedHandler : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var message = context.Message;

			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("\n--->> Received order created event: OrderId: {0}\n", message.OrderId);
			Console.ResetColor();

			var cardDetails = Database.GetCardDetailsFor(message.UserId);
			var confirmation = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);

			// NOTE_JBOY: Chris Patterson prefers not use .Send(), but .Publish() instead - https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
			await context.Publish<RecordPaymentAttempt>(new
			{
				OrderId = message.OrderId,
				Status = confirmation.Status
			});
		}
    }

	// NOTE: this static PaymentProvider class is for demo purposes only
	public static class PaymentProvider
	{
		private static int Attempts = 0;

		public static PaymentConfirmation ChargeCreditCard(CardDetails details, double amount)
		{
			//if (Attempts < 2)
			//{
			//	Attempts++;
			//	throw new Exception("Service unavailable. Down for maintenance.");
			//}
			return new PaymentConfirmation { Status = PaymentStatus.Accepted };
		}
	}

	public class PaymentConfirmation
	{
		public PaymentStatus Status { get; set; }
	}

	// NOTE: this static Database class is for demo purposes only
	public static class Database
	{
		public static CardDetails GetCardDetailsFor(string userId)
		{
			return new CardDetails();
		}
	}

	public class CardDetails
	{
		// ...
	}
}
