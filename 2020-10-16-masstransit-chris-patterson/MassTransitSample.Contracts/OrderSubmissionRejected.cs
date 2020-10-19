using System;

namespace MassTransitSample.Contracts
{
	public interface OrderSubmissionRejected
	{
		public Guid OrderId { get; set; }
		public DateTime Timestamp { get; set; }

		public string CustomerNumber { get; set; }

		public string Reason { get; set; }
	}
}
