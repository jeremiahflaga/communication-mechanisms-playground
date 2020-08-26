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
            Console.WriteLine("Received order created event: OrderId: {0}", message.OrderId);
            
            //var cardDetails = Database.GetCardDetailsFor(message.UserId);
            //var confirmation = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);
            //var command = new RecordPaymentAttempt
            //{
            //    OrderId = message.OrderId,
            //    Status = confirmation.Status
            //};
            //context.Send(command);
        }
    }

    //public static class PaymentProvider
    //{
    //    private static int Attempts = 0;

    //    public static PaymentConfirmation ChargeCreditCard(CardDetails details, double amount)
    //    {
    //        if (Attempts < 2)
    //        {
    //            Attempts++;
    //            throw new Exception("Service unavailable. Down for maintenance.");
    //        }
    //        return new PaymentConfirmation { Status = PaymentStatus.Accepted };
    //    }
    //}

    //public class PaymentConfirmation
    //{
    //    public PaymentStatus Status { get; set; }
    //}

    //public static class Database
    //{
    //    public static CardDetails GetCardDetailsFor(string userId)
    //    {
    //        return new CardDetails();
    //    }
    //}

    //public class CardDetails
    //{
    //    // ...
    //}
}
