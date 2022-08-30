using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Repositories;

namespace WebApiCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
      
        public CustomersController(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listCustomers = await _customerRepository.GetAllAsync();
            var result = _mapper.Map<List<CustomerReadDto>>(listCustomers);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var customerItem = await _customerRepository.GetAsync(id);
            var result = _mapper.Map<CustomerReadDto>(customerItem);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerCreateDto customerCreateDto)
        {
            var customerToInsert = _mapper.Map<Customer>(customerCreateDto);
            await _customerRepository.AddAsync(customerToInsert);
            await _customerRepository.SaveAsync();
            return CreatedAtAction("Get", new { id = customerToInsert.Id }, customerToInsert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
        {
            if (id != customerUpdateDto.Id) return BadRequest();
            var customerToUpdate = _mapper.Map<Customer>(customerUpdateDto);
            await _customerRepository.UpdateAsync(customerToUpdate);
            await _customerRepository.SaveAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerRepository.DeleteAsync(id);
            await _customerRepository.SaveAsync();
            return NoContent();
        }



    }
}
