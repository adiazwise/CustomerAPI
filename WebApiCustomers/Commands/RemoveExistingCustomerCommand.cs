using MediatR;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Commands;

public sealed record RemoveExistingCustomerCommand (int Id) : IRequest;