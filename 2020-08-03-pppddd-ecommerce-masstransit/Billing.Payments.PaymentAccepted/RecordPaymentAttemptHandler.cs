using Billing.Messages.Commands;
using MassTransit;
using Sales.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Payments.PaymentAccepted
{
	public class RecordPaymentAttemptHandler : IConsumer<RecordPaymentAttempt>
	{
		public async Task Consume(ConsumeContext<RecordPaymentAttempt> context)
        {
            Database.SavePaymentAttempt(context.Message.OrderId, context.Message.Status);
            if (context.Message.Status == PaymentStatus.Accepted)
            {
                await context.Publish<Messages.Events.PaymentAccepted>(new
                {
                    OrderId = context.Message.OrderId
                });

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(
                    "Received payment accepted notification for Order: {0}. Published PaymentAccepted event",
                    context.Message.OrderId
                );
                Console.ResetColor();
            }
            else
            {
                // publish a payment rejected event
            }
        }

        // NOTE: this static Database class is for demo purposes only
        public static class Database
		{
			public static void SavePaymentAttempt(string orderId, PaymentStatus status)
			{
				// .. save it to your favorite database
			}
		}
	}
}
