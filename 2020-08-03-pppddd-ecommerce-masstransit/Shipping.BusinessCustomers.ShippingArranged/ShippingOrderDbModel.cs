﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public class ShippingOrderDbModel
    {
        public string UserId { get; set; }

        public string OrderId { get; set; }

        public string ShippingTypeId { get; set; }

        public string AddressId { get; set; }
    }
}
