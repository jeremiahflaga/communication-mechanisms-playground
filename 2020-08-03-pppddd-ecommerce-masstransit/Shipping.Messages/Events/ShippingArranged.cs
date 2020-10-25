using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.Messages.Events
{
    public interface ShippingArranged
    {
        public string OrderId { get; set; }

        /*
         * Other fields, such as date/date range 
         * could be added here depending on your 
         * shipping provider(s) API
         */
    }
}
