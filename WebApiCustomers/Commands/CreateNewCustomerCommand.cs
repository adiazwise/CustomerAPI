using MediatR;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Commands;
public sealed record CreateNewCustomerCommand (CustomerCreateDto customerToCreate) : IRequest<CustomerReadDto>;