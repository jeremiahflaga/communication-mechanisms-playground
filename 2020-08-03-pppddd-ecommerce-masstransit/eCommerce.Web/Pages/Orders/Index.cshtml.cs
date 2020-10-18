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
		private readonly ISendEndpointProvider sendEndpointProvider;

		public IndexModel(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            this.publishEndpoint = publishEndpoint;
			this.sendEndpointProvider = sendEndpointProvider;
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

            sendEndpointProvider.GetSendEndpoint
            await sendEndpointProvider.Send<PlaceOrder>(placeOrderCommand);
            //await publishEndpoint.Publish<PlaceOrder>(placeOrderCommand);

            return Content("Your order has been placed. You will receive email confirmation shortly.");
        }
    }
}