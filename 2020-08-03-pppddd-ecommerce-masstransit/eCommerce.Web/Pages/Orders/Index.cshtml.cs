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
			// NOTE_JBOY: use .Publish() because .Send() needs special configuration when the consumer services is inside a Docker container 
			await publishEndpoint.Publish<PlaceOrder>(new
            {
                UserId = userId,
                ProductIds = productIds.Split(','),
                ShippingTypeId = shippingTypeId,
                TimeStamp = DateTime.Now
            });

			// NOTE_JBOY: Chris Patterson prefers not use .Send() - https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
			// ... becuase when using .Send() we will need to configure it in the Startup class, e.g. EndpointConvention.Map<PlaceOrder>(new Uri("queue:place-order-handler"));
			// But .Send() is used in here for demo purposes
			//var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:place-order-handler"));
			//await endpoint.Send<PlaceOrder>(placeOrderCommand);
			//await sendEndpointProvider.Send<PlaceOrder>(new
            //{
            //    UserId = userId,
            //    ProductIds = productIds.Split(','),
            //    ShippingTypeId = shippingTypeId,
            //    TimeStamp = DateTime.Now
            //});

			return Content("Your order has been placed. You will receive email confirmation shortly.");
        }
    }
}