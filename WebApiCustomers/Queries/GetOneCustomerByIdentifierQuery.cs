using MediatR;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Queries;
public sealed record GetOneCustomerByIdentifierQuery (int Id)  : IRequest<CustomerReadDto>;