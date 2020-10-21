using MassTransit;
using MassTransitSample.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitSample.Components.Consumers
{
	public class SubmitOrderConsumer : IConsumer<SubmitOrder>
	{
		private readonly ILogger<SubmitOrderConsumer> logger;

		public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
		{
			this.logger = logger;
		}

		public async Task Consume(ConsumeContext<SubmitOrder> context)
		{
			logger.Log(LogLevel.Debug, "SubmitOrderConsumer: {CustomerNumber}", context.Message.CustomerNumber);

			if (context.Message.CustomerNumber.Contains("TEST"))
			{
				if (context.RequestId != null) // or context.ResponseAddress != null
				{
					await context.RespondAsync<OrderSubmissionRejected>(new
					{
						InVar.Timestamp,
						context.Message.OrderId,
						context.Message.CustomerNumber,
						Reason = $"Test Customer cannot submit orders: {context.Message.CustomerNumber}"
					});
				}

				return;
			}

			if (context.RequestId != null) // or context.ResponseAddress != null
			{
				await context.RespondAsync<OrderSubmissionAccepted>(new
				{
					InVar.Timestamp,
					context.Message.OrderId,
					context.Message.CustomerNumber
				});
			}
		}
	}
}
