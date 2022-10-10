## How to Build a Web API ASP.NET Core 6 Part 2

In the previous post, we discussed how to build a web API applying useful patterns like dependency injection, repository, and some NuGet packages like automapper [Part 1](https://dev.to/learnwithandres/how-to-build-a-web-api-aspnet-core-6-2doc).
At this time, we are continuing with CQRS and Mediator patterns.

Before starting with visual studio, we explain what kind of problems resolve CQRS and Mediator patterns.

### CQRS

CQRS are the acronyms of Command Query and Responsibility Segregation. This pattern pursues how to separate read and update operations for CRUD. However, be careful not to apply for any project and add extra complexity because this pattern is not bulletproof.


### Why use CQRS


In common architectures, the typical case is to use the same model for query and update databases, which works perfectly for simple CRUD operations. But in complex projects, it can be tricky. To be more explicit, we would need to perform different queries returning specific Dtos, and the write models set multiples and complex validations following the business logic.


Can you check more about  [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)


### Mediator Pattern

This pattern is part of the family of the behavior design pattern, and the main goal is coordinate the relations between associate objects. And is help us to define how to encapsulate the integration of each of these different objects. On the other hand, it Promotes low coupling to prevent these objects from being referenced explicitly and allows to change dependency easier between them.

### Why  use Mediator

In this case, we need to use this pattern because we are creating a mechanism to communicate all requests to use queries or commands through the handlers.
The handler is the class where you create the task, follow the queries or commands requirements, accessing the storage through repositories.

**MediatR**

But to facilitate the implementation of these patterns described before, we are using these NuGet packages.

*MediatR
*MediatR.Extensions.Microsoft.DependencyInjection


Before to start creating commands and query models, we are adding the service MediatAr to the program.cs file like this way :
builder.Services.AddMediatR(Assembly.GetCallingAssembly());

**Step 1**
Now We have to build different types of elements for the queries, commands, and handlers. Therefore we have to add these folders to keep a clean structure.

/queries
/commands
/handlers

The next step is to start defining the queries and command models. 

**Tips:**

Commands: should be defined like a simple name task and not describe action data on the database.
Example “CreateNewCustomer” instead of “InsertCustomer”
Queries: the queries never update the database. Only could return dtos with the data requested.

Before creating commands and query models, we add the service MediatAr to the program.cs file like this way :
builder.Services.AddMediatR(Assembly.GetCallingAssembly());

Before creating commands and query models, we add the service MediatAr to the program.cs file like this way : 

builder.Services.AddMediatR(Assembly.GetCallingAssembly());



**Step 2**

### Queries

Now Let’s go to create the query models. 

In this case, we are creating the query models, so we will be required to use the IRequest interface with the dto to return.

In this case, we take advantage of [records](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record). In this way, we don't have to define a constructor and keep the immutability.

#### GetAllCustomersQuery

```CS
using MediatR;
using WebApiCustomers.Dtos;
namespace WebApiCustomers.Queries;
public sealed record GetAllCustomersQuery : IRequest<List<CustomerReadDto>>;
```

#### GetOneCustomerByIdentifierQuery

```CS
using MediatR;
using WebApiCustomers.Dtos;
 
namespace WebApiCustomers.Queries;
public sealed record GetOneCustomerByIdentifierQuery (int Id)  :IRequest<CustomerReadDto>;
```

### Commands

In this step, we continue creating the commands. 

#### CreateNewCustomerCommand
In this case, we are creating a simple record that returns the CustomerCreateDto object.

```CS
using MediatR;
using WebApiCustomers.Dtos;
 
namespace WebApiCustomers.Commands;
public sealed record CreateNewCustomerCommand (CustomerCreateDto customerToCreate) : IRequest<CustomerReadDto>;
 ```


#### EditExistingCustomerCommand

In this case, the IRequest interface has no parameters because we don't want to return anything. 

```CS
using MediatR;
using WebApiCustomers.Dtos;
 
namespace WebApiCustomers.Commands;
 
public sealed record EditExistingCustomerCommand (CustomerUpdateDto customerToUpdate) : IRequest;
```

#### RemoveExistingCustomer

We have the same signature in this command because we don't want to return anything either.

```CS
using MediatR;
using WebApiCustomers.Dtos;
namespace WebApiCustomers.Commands;
public sealed record RemoveExistingCustomerCommand (int Id) : IRequest;
```

**Step 3**

Now we are ready to add our handlers for queries and command models.

### Handlers

The handlers are responsible for receiving the request and encapsulating the functionality to accomplish the incoming request with specific signatures that may change if they are queries or command requests inherited from the IRequestHandler interface.

Now we are defining these handlers.

#### GetAllCustomersHandler
On this handler, we are using the repository to get all customers and mapping the object to the requested object CustomerReadDto
```CS
using AutoMapper;
using MediatR;
using WebApiCustomers.Dtos;
using WebApiCustomers.Queries;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Handlers;
 
public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerReadDto>>
{
   private readonly ICustomerRepository _customerRepository;
   private IMapper _mapper;
   public GetAllCustomersHandler(ICustomerRepository customerRepository, IMapper mapper)
   {
       _customerRepository = customerRepository;
       _mapper = mapper;
   }
   public async Task<List<CustomerReadDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
   {
       var customerList = await _customerRepository.GetAllAsync();
       return _mapper.Map<List<CustomerReadDto>>(customerList);
   }
}
```


#### GetOneCustomerByIdentifierHandler

On this handler, we are using the repository to get one customer by identifier and mapping the requested object CustomerReadDto.

```CS
Using AutoMapper;
using MediatR;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Queries;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Handlers;
 
public sealed class GetOneCustomerByIdentifierHandler :
IRequestHandler<GetOneCustomerByIdentifierQuery, CustomerReadDto>
{
   private readonly ICustomerRepository _customerRepository;
   private readonly IMapper _mapper;
 
   public GetOneCustomerByIdentifierHandler(ICustomerRepository customerRepository, IMapper mapper )
   {
       _customerRepository = customerRepository;
       _mapper = mapper;
   }
   public async Task<CustomerReadDto> Handle(GetOneCustomerByIdentifierQuery request, CancellationToken cancellationToken)
   {
       var customer = await _customerRepository.GetAsync(request.Id);
       return _mapper.Map<CustomerReadDto>(customer);
   }
}
 ```

#### CreateNewCustomerHandler

On this handler, we are using the repository to create a new customer and mapping the object requested object CustomerReadDto.
 
```CS
using AutoMapper;
using MediatR;
using WebApiCustomers.Commands;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Handlers;
public class CreateNewCustomerHandler : IRequestHandler<CreateNewCustomerCommand, CustomerReadDto>
{
   private readonly ICustomerRepository _customerRepository;
   private readonly IMapper _mapper;
 
   public CreateNewCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
   {
       _customerRepository = customerRepository;
       _mapper = mapper;
   }
   public async Task<CustomerReadDto?> Handle(
       CreateNewCustomerCommand request,
       CancellationToken cancellationToken)
   {
         var customerToInsert = _mapper.Map<Customer>(request.customerToCreate);
           await _customerRepository.AddAsync(customerToInsert);
           await _customerRepository.SaveAsync();
           var result  = _mapper.Map<CustomerReadDto>(customerToInsert);
          
           return result == null ? null : await Task.FromResult(result);
 
   }
}
 ```

#### EditExistingCustomerHandler

 On this handler, we are using the repository to edit the requested and return the Unit object because we don't need to return it.

 ```CS
using AutoMapper;
using MediatR;
using WebApiCustomers.Commands;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Handlers;
public sealed class EditExistingCustomerHandler : IRequestHandler<EditExistingCustomerCommand, Unit>
{
   private readonly ICustomerRepository _customerRepository;
   private readonly IMapper _mapper;
 
   public EditExistingCustomerHandler(ICustomerRepository customerRepository,
                                   IMapper mapper )
   {
        _customerRepository = customerRepository;
        _mapper = mapper;
   }
   public async Task<Unit> Handle(EditExistingCustomerCommand request, CancellationToken cancellationToken)
   {
       var customerToUpdate =  _mapper.Map<Customer>(request.customerToUpdate);
       await _customerRepository.UpdateAsync(customerToUpdate);
       return Unit.Value;
   }
}
```

### RemoveExistingCustomerHandler

On this handler, we are using the repository to remove the requested customer and return the Unit object when we don't need to return it.

```CS
using AutoMapper;
using MediatR;
using WebApiCustomers.Commands;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Handlers;
 
public sealed class RemoveExistingCustomerHandler : IRequestHandler<RemoveExistingCustomerCommand, Unit>
{
   private readonly ICustomerRepository _customerRepository;
   private readonly IMapper _mapper;
 
   public RemoveExistingCustomerHandler(ICustomerRepository customerRepository,
                                       IMapper mapper)
   {
       _customerRepository = customerRepository;
       _mapper = mapper;
   }
   public async Task<Unit> Handle(RemoveExistingCustomerCommand request, CancellationToken cancellationToken)
   {
           await _customerRepository.DeleteAsync(request.Id);
           await _customerRepository.SaveAsync();
 
           return Unit.Value;
   }
}
```
### Controller

In the last step, we update the controller class to remove unnecessary injected objects and use the method “Send” from the mediator object.
 
```CS
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCustomers.Commands;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Queries;
using WebApiCustomers.Repositories;
 
namespace WebApiCustomers.Controllers;
 
   [Route("api/[controller]")]
   [ApiController]
   public class CustomersController : ControllerBase
   {
       private readonly IMediator _mediator;
 
       public CustomersController(IMediator mediator)
       {
          _mediator = mediator;
          
       }
 
       [HttpGet]
       public async Task<IActionResult> GetAll()
       {
         
           var result  = await _mediator.Send( new GetAllCustomersQuery());
           return Ok(result);
       }
 
       [HttpGet("{id:int}")]
       public async Task<IActionResult> Get(int id)
       {
          
           var result = await _mediator.Send(new GetOneCustomerByIdentifierQuery(id));
           return result == null ? NotFound() : Ok(result);
 
       }
 
       [HttpPost]
       public async Task<IActionResult> PostCustomer([FromBody] CustomerCreateDto customerCreateDto)
       {
           var result = await _mediator.Send(
               new CreateNewCustomerCommand(customerCreateDto));
          
           return CreatedAtAction("Get", new { id = result.Id }, result);
       }
 
       [HttpPut("{id}")]
       public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
       {
           if (id != customerUpdateDto.Id) return BadRequest();
 
           await _mediator.Send(new EditExistingCustomerCommand(customerUpdateDto));
          
           return NoContent();
       }
 
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCustomer(int id)
       {
           await _mediator.Send(new RemoveExistingCustomerCommand(id));
           return NoContent();
       }
   }
``` 

## Conclusion

We learned about these topics.

* CQRS and Mediator Patterns.
* How to separate Queries and Commands Models
* How to Implements the MediatR Nuget Package.


Source code [WebApiCustomers Part 2](https://github.com/adiazwise/CustomerAPI.git)

If you enjoyed this article, please subscribe and follow this series about Building clean web API Net Core 6.
 



