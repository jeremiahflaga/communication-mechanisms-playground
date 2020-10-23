using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.Messages.Commands
{
	public interface RecordPaymentAttempt
	{
		public string OrderId { get; set; }
		public PaymentStatus Status { get; set; }
	}

	public enum PaymentStatus
	{
		Accepted,
		Rejected
	}
}
