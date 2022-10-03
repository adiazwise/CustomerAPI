using MediatR;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Queries;
public record GetAllCustomersQuery : IRequest<List<CustomerReadDto>>;