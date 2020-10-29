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
    public class DetailsModel : PageModel
    {
		public DetailsModel()
		{
		}

        public int OrderId { get; set; }

        public void OnGet(int orderId)
        {
            OrderId = orderId;
        }
    }
}
