In the previous post we talked about how to build a web api appling useful patterns like dependency injection, repository and  some nuget packages like automapper [Part 1](https://dev.to/learnwithandres/how-to-build-a-web-api-aspnet-core-6-2doc).


At this time we are continuing with CQRS and Mediator patterns.

Before starting with visual studio,we are explaining what kind of problems resolve CQRS and Mediator patterns.

### CQRS 

CQRS are the acronyms of Command Query and Responsibility Segregation,  this pattern pursues how to separate read and update operations for CRUD, but be careful not to apply for any project and add extra complexity because this pattern is not bulletproof.

***Why to use CQRS*** 
In common architectures, the typical case is to use the same model for query and update databases, which works perfectly for simple CRUD operations. But in complex projects, it can be tricky. To be more explicit,  we would need to perform different queries returning specific Dtos, and the write models set multiples and complex validations following the business logic. 



Can you check more about  [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)


### Mediator Pattern

This pattern is part of the family of the behavior design pattern, and the main goal is coordinate the relations between associate objects. And is help us to define how to encapsulate the integration of each of these different objects. On the other hand, it Promotes low coupling to prevent these objects from being referenced explicitly and allows to change dependency easier between them.


**Why To use Mediator**

In this case, we need to use this pattern because we are creating a mechanism to communicate all requests to use queries or commands through the handlers.

The handler is the class where you create the task, following the queries or commands requirements, accessing the storage through repositories.

MediatR

 But to facilitate the implementation of these patterns described before, we are using these nuget packages.


* MediatR
* MediatR.Extensions.Microsoft.DependencyInjection


