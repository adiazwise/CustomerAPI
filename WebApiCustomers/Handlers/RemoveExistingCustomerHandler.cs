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