# A simple proof-of-concept project using MassTransit: Rewriting in .NET Core 3 the sample project of chapter 12 of PPPDDD (uses MassTransit with RabbitMQ, Razor Pages)

## References:

- Chapter 12 of ["Patterns, Principles, and Practices of Domain-Driven Design"](https://www.bookdepository.com/Patterns-Principles-Practices-Domain-Driven-Design-Scott-Millett/9781118714706?a_aid=jflaga) by Scott Millett
	- Uses .NET Framework 4.5, NServiceBus 4.3.3, MassTransit with MSMQ
	- Source code: https://github.com/elbandit/PPPDDD/tree/master/12%20-%20Integrating%20Via%20Messaging

- MassTransit documentation: [Configuration page](https://masstransit-project.com/usage/configuration.html)

## How to run

1. Download and install RabbitMQ: https://www.rabbitmq.com/download.html

2. Open `eCommerce.sln` in Visual Studio 2019

3. Set Multiple Startup Projects
	- Billing.Payments.PaymentAccepted
	- Sales.Orders.OrderCreated
	- **Last:** eCommerce.Web

4. Run in VS 2019
