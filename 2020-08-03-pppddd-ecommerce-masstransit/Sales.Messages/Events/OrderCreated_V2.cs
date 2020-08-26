using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Messages.Events
{
    public class OrderCreated_V2 : OrderCreated
    {
        public string AddressId { get; set; }
    }
}
