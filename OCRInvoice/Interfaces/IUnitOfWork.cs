using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace OCRInvoice.Interfaces
{
    public interface IUnitOfWork
    {
        IInvoiceMasterRepository InvoiceMaster { get; }
        ILineItemMasterRepository LineItemMaster { get; }

        ICustomerRepository Customer { get; }
        Task SaveAsync(); 
    }
}
