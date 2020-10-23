using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.Messages.Events
{
	public interface PaymentAccepted
	{
		public string OrderId { get; set; }
	}
}
