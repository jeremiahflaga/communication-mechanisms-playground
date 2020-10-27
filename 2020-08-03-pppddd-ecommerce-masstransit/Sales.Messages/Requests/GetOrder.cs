using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Messages.Requests
{
	// NOTE: Request/response is a common pattern in application development, where a component sends a request to a service 
	// and continues once the response is received. In a distributed system, this can increase the latency of an application 
	// since the service may be hosted in another process, on another machine, or may even be a remote service in another 
	// network. While in many cases it is best to avoid request/response use in distributed applications, particularly when 
	// the request is a command, it is often necessary and preferred over more complex solutions. 
	// - https://masstransit-project.com/usage/requests.html#request-consumer
	public interface GetOrder
	{
		public string OrderId { get; set; }
	}

	public interface GetOrderResult
	{
		public string OrderId { get; set; }

		public string UserId { get; set; }

		public string[] ProductIds { get; set; }

		public string ShippingTypeId { get; set; }

		public DateTime TimeStamp { get; set; }

		public double Amount { get; set; }
	}
}
