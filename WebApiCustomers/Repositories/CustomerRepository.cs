using Microsoft.EntityFrameworkCore;
using WebApiCustomers.Data;

namespace WebApiCustomers.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDemoDbContext context) : base(context) => _context = context;
      
    }
}
