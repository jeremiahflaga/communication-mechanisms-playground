using MassTransit;
using MassTransitSample.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitSample.Components.Consumers
{
	public class SubmitOrderConsumer : IConsumer<SubmitOrder>
	{
		public async Task Consume(ConsumeContext<SubmitOrder> context)
		{
			await context.RespondAsync<OrderSubmissionAccepted>(new
			{
				InVar.Timestamp
,
				OrderId = context.Message.OrderId,
				CustomerNumber = context.Message.CustomerNumber
			});
		}
	}
}
