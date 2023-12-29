using OCRInvoice.Entities;
using OCRInvoice.Interfaces;

namespace OCRInvoice.Services
{
    public class InvoiceImageRepository : GenericRepository<InvoiceImage>, IInvoiceImageRepository
    {
        public InvoiceImageRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
