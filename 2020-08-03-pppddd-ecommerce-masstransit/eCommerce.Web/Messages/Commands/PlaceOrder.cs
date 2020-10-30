using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Messages.Commands
{
    public interface PlaceOrder
    {
        public string UserId { get; set; }

        public string[] ProductIds { get; set; }

        public string ShippingTypeId { get; set; }

        public DateTime TimeStamp { get; set; }
    }

    public interface PlaceOrderResult
	{
		public PlaceOrderStatus Status { get; set; }
	}

    public enum PlaceOrderStatus
	{
        Accepted,
        Rejected
	}
}
