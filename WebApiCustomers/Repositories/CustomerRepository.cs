using Microsoft.EntityFrameworkCore;
using WebApiCustomers.Data;

namespace WebApiCustomers.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerDemoDbContext _context; 
        public CustomerRepository(CustomerDemoDbContext context) : base(context) => _context = context;
      
    }
}
