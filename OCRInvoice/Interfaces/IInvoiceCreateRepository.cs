using OCRInvoice.Models;
using OCRInvoice.Models.Request;

namespace OCRInvoice.Interfaces
{
    public interface IInvoiceCreateRepository
    {
        Task<ApiResponse> CreateInvoice(InvoiceOcrRequest request);
    }
}
