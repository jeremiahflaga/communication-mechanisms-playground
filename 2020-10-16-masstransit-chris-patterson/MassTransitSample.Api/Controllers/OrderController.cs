﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MassTransitSample.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> logger;
		private readonly IRequestClient<SubmitOrder> submitOrderRequestClient;

		public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> submitOrderRequestClient)
		{
			this.logger = logger;
			this.submitOrderRequestClient = submitOrderRequestClient;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Guid id, string customerNumber)
		{
			var response = await submitOrderRequestClient.GetResponse<OrderSubmissionAccepted>(new
			{
				OrderId = id,
				Timestamp = InVar.Timestamp,
				CustomerNumber = customerNumber
			});

			return Ok(response.Message);
		}
	}
}
