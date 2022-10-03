using AutoMapper;
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
