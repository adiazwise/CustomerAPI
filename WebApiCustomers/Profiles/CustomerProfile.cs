using AutoMapper;
using WebApiCustomers.Data;
using WebApiCustomers.Dtos;

namespace WebApiCustomers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerCreateDto, Customer>().
                ForMember(m => m.CreatedAt, o => o.MapFrom(s => DateTime.Now.Date));
            CreateMap<CustomerUpdateDto, Customer>();

        }
    }
}
