# Client and Services Communication Mechanisms Playground

**Synchronous Communication Keywords:** AsyncAPI, 

**Asynchronous Cmmunication Keywords:** MassTransit, RabbitMQ, 






### Background on why this repo is named "Communication Mechanisms Playground"

I was learning DDD throug ["Patterns, Principles, and Practices of Domain-Driven Design"](https://www.bookdepository.com/Patterns-Principles-Practices-Domain-Driven-Design-Scott-Millett/9781118714706?a_aid=jflaga) by Scott Millett with Nick Tune

I asked Nick Tune on twitter:


> Hi Nick, I have read this answer of yours for a question on your book, PPPDDD: 
> 
> "... each bounded context is a separate software application/component/service deployed independently. Therefore, they cannot share the same instance of Bus class because they are running in different processes, probably on different servers." (from https://p2p.wrox.com/book-patterns-principles-practices-domain-driven-design/94561-communication-between-bcs-nsb.html…) 
> 
> I have another question regarding the code in Chapter 12. The `Billing.Messages` which resides in the Billing Context is being referenced by `Shipping.BusinessCustomers.ShippingArranged` which resides in the Shipping Context. 
> 
> If the Billing Context and Shipping Context are being maintained by different teams, does this mean the Billing team will have to provide a copy of the DLL of the `Billing.Messages` module each time they make a change to it? 


His reply:

> Hi Jeremiah. Good question.
> There are a few ways of sharing contracts.
> Nowadays I prefer not to share code as schema
> Because it only works in heterogenous systems and it risks logic being added into the shared code.
>
> The industry is moving towards open standards now. Have you seen: https://www.asyncapi.com/docs/getting-started/hello-world/

And so I googled for AsyncAPI, and found the article ["Welcome to the AsyncAPI Revolution!"](https://medium.com/analytics-vidhya/welcome-to-the-asyncapi-revolution-d5df347ebc21)

> Do you still remember the old ages when SOAP was the unbeatable format? How much time did we spend building our SOAP Services in our enterprises? REST replace it as the new standard.. but just a few years and we’re back in a new battle just for **synchronous communication: gRPC, GraphQL**, are here to conquer everything again. It is crazy, huh?
> 
> But the situation is similar to **asynchronous communication**.
>
>
> If we think about REST, gRPC, or GraphQL even that they’re different they play based some common grounds. They rely on the same base HTTP protocol for a standard so it is easy to support all of them in the same architecture.
>
> But this is not true with the technologies about Asynchronous Communication. And I’d like to focus on standardization and specification today. And that’s what AsyncAPI Initiative is trying to solve. And to define what AsyncAPI is I’d like to use their own words from their official website:
>
> >AsyncAPI is an open source initiative that seeks to improve the current state of Event-Driven Architectures (EDA). Our long-term goal is to make working with EDA’s as easy as it is to work with REST APIs. That goes from documentation to code generation, from discovery to event management. Most of the processes you apply to your REST APIs nowadays would be applicable to your event-driven/asynchronous APIs too.

Another reference which I read a few days ago (by Aug 26, 2020) while learning Microservices in .NET: ["Communication in a microservice architecture"](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/architect-microservice-container-applications/communication-in-microservice-architecture)

> The biggest challenge when changing from a monolithic application to a microservices-based application lies in changing the **communication mechanism**.

> **Client and services can communicate through many different types of communication**, each one targeting a different scenario and goals.

