using MediatR;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Commands;

public sealed record EditExistingCustomerCommand (CustomerUpdateDto customerToUpdate) : IRequest;