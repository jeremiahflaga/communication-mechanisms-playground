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

            // NOTE_JBOY: This .Publish() code is an old code which is replaced by .Send() in here
            //await publishEndpoint.Publish<PlaceOrder>(placeOrderCommand);
            
            // NOTE_JBOY: Chris Patterson does not use .Send() - https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
            //var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:place-order-handler"));
            await sendEndpointProvider.Send<PlaceOrder>(placeOrderCommand);

			return Content("Your order has been placed. You will receive email confirmation shortly.");
        }
    }
}