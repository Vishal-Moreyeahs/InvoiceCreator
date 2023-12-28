using OCRInvoice.Entities;
using OCRInvoice.Interfaces;
using OCRInvoice.Models.Request;

namespace OCRInvoice.Services
{
    public class InvoiceCreateRepository : IInvoiceCreateRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceCreateRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateInvoice(InvoiceOcrRequest invoice)
        {
            try
            { 
                if (invoice == null)
                { 
                    return false;
                }
                var data = invoice.Worksheet1.FirstOrDefault();
                var customer = new Customer
                {
                    Name = data.ReceiverName,
                    Address = data.ReceiverAddress,
                    TaxId = data.ReceiverGSTNumber
                };
                var invoiceMaster = new InvoiceMaster
                {
                    InvoiceNumber = data.InvoiceNumber,
                    Date = DateTime.Parse(data.InvoiceDate),
                    Address = data.SenderAddress,
                    ProviderName = data.SenderName,
                    TaxId = data.SenderGSTNumber,
                    Total = data.TotalAmount
                };

                var isCustomerAdded = await _unitOfWork.Customer.Add(customer);
                var isInvoiceMasterAdded = await _unitOfWork.InvoiceMaster.Add(invoiceMaster);
                await _unitOfWork.SaveAsync();

                if (isCustomerAdded && isInvoiceMasterAdded)
                {
                    foreach (var item in invoice.Worksheet2)
                    {
                        var lineItem = new LineItemMaster
                        {
                            ItemName = item.ItemName,
                            Price = item.ItemRate,
                            InvoiceID = invoiceMaster.InvoiceID,
                            Qty = item.ItemQuantity,
                            Tax = item.ItemTax,
                        };
                        var isLineItemsAdded = await _unitOfWork.LineItemMaster.Add(lineItem);
                        if (isLineItemsAdded)
                        {
                            await _unitOfWork.SaveAsync();
                        }
                    }
                    return true;
                }
                else
                { 
                    return false;
                }
            }
            catch(Exception ex)
            { 
                return false; 
            }
        }
    }
}
