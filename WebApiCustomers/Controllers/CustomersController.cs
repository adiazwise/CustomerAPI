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

