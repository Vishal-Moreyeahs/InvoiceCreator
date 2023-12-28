using OCRInvoice.Entities;
using OCRInvoice.Interfaces;
using System.Numerics;

namespace OCRInvoice.Services
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
