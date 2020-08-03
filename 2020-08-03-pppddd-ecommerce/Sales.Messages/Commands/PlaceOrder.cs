using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Messages.Commands
{
    public class PlaceOrder
    {
        public string UserId { get; set; }

        public string[] ProductIds { get; set; }

        public string ShippingTypeId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
