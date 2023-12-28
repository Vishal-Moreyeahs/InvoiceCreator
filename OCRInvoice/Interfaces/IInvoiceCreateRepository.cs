using OCRInvoice.Models.Request;

namespace OCRInvoice.Interfaces
{
    public interface IInvoiceCreateRepository
    {
        Task<bool> CreateInvoice(InvoiceOcrRequest request);
    }
}
