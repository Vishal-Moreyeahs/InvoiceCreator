namespace OCRInvoice.Models.Request
{
    public class UploadInvoice
    {
        public int InvoiceId { get; set; }
        public IFormFile Invoice { get; set;}
    }
}
