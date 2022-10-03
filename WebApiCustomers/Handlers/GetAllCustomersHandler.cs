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