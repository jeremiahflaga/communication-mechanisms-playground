using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public static class ShippingDatabase
    {
        private static List<ShippingOrderDbModel> Orders = new List<ShippingOrderDbModel>();

        public static void AddOrderDetails(ShippingOrderDbModel order)
        {
            Orders.Add(order);
        }

        public static string GetCustomerAddress(string orderId)
        {
            var order = Orders
                        .Single(o => o.OrderId == orderId);

            return string.Format(
                "{0}, Address ID: {1}",
                order.UserId, order.AddressId
            );
        }
    }

}
