using AutoMapper;
using MediatR;
using WebApiCustomers.Commands;
using WebApiCustomers.Repositories;

namespace WebApiCustomers.Handlers;

public sealed class RemoveExistingCustomerHandler : IRequestHandler<RemoveExistingCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public RemoveExistingCustomerHandler(ICustomerRepository customerRepository,
                                        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    public async Task Handle(RemoveExistingCustomerCommand request, CancellationToken cancellationToken)
    {
            await _customerRepository.DeleteAsync(request.Id);
            await _customerRepository.SaveAsync();
            return;
    }   
}