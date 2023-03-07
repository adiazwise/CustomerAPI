
using AutoMapper;
using MediatR;
using WebApiCustomers.Commands;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;
using WebApiCustomers.Repositories;

namespace WebApiCustomers.Handlers;
public sealed class EditExistingCustomerHandler : IRequestHandler<EditExistingCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public EditExistingCustomerHandler(ICustomerRepository customerRepository, 
                                    IMapper mapper )
    {
         _customerRepository = customerRepository;
         _mapper = mapper;
    }
    public async Task Handle(EditExistingCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerToUpdate =  _mapper.Map<Customer>(request.customerToUpdate);
        await _customerRepository.UpdateAsync(customerToUpdate);
        return;
    }
}