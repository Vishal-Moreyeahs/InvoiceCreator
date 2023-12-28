using OCRInvoice.Entities;
using OCRInvoice.Interfaces;
using System.Numerics;

namespace OCRInvoice.Services
{
    public class InvoiceMasterRepository : GenericRepository<InvoiceMaster>, IInvoiceMasterRepository

    {
        public InvoiceMasterRepository(DataBaseContext context) : base(context)
        {
        }
    }
}
