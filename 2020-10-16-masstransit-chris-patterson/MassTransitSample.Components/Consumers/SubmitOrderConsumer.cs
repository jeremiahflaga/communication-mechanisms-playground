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
		public Task Consume(ConsumeContext<SubmitOrder> context)
		{
			throw new NotImplementedException();
		}
	}
}
