using Microsoft.EntityFrameworkCore;
using OCRInvoice.Entities;
using OCRInvoice.Interfaces;

namespace OCRInvoice.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataBaseContext _context;

        public ICustomerRepository Customer { get; private set; }

        public IInvoiceMasterRepository InvoiceMaster { get; private set; }

        public ILineItemMasterRepository LineItemMaster { get; private set; }


        public UnitOfWork(
            DataBaseContext context
        )
        {
            _context = context;

            LineItemMaster = new LineItemMasterRepository(_context);
            Customer = new CustomerRepository(_context);
            InvoiceMaster = new InvoiceMasterRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
