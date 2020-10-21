using System;

namespace MassTransitSample.Contracts
{
	public interface OrderSubmissionAccepted
	{
		public Guid OrderId { get; set; }
		public DateTime Timestamp { get; set; }

		public string CustomerNumber { get; set; }
	}
}
