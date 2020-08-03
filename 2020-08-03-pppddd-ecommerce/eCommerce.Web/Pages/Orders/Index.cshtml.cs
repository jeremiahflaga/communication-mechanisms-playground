using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.Messages.Commands;

namespace eCommerce.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IPublishEndpoint publishEndpoint;

        public IndexModel(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string userId, string productIds, string shippingTypeId)
        {
            var realProductIds = productIds.Split(',');
            var placeOrderCommand = new PlaceOrder
            {
                UserId = userId,
                ProductIds = realProductIds,
                ShippingTypeId = shippingTypeId,
                TimeStamp = DateTime.Now
            };

            //MvcApplication.Bus.Send("Sales.Orders.OrderCreated", placeOrderCommand);
            await publishEndpoint.Publish<PlaceOrder>(placeOrderCommand);

            return Content("Your order has been placed. You will receive email confirmation shortly.");
        }
    }
}