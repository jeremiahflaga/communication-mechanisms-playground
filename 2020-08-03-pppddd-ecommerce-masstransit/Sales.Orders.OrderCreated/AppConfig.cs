namespace Sales.Orders.OrderCreated
{
	class AppConfig
	{
		public RabbitMqConfig RabbitMq { get; set; }
	}

	class RabbitMqConfig
	{
		public string HostAddress { get; set; }
		public string VirtualHost { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
