using OCRInvoice.Entities;
using OCRInvoice.Enums;
using OCRInvoice.Interfaces;
using OCRInvoice.Models;
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

        public async Task<ApiResponse> CreateInvoice(InvoiceOcrRequest invoice)
        {
            try
            {
                var lineItemMasters = new List<LineItemMaster>();
                if (invoice == null || invoice.Worksheet1.Count==0 || invoice.Worksheet2.Count == 0)
                { 
                    return new ApiResponse { 
                        StatusCode = (int)StatusCode.BadRequest,
                        Success = false,
                        Message = "Request Body is incorrect"
                    };
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
               
                if (isCustomerAdded && isInvoiceMasterAdded)
                {
                    await _unitOfWork.SaveAsync();
                    foreach (var item in invoice.Worksheet2)
                    {
                        lineItemMasters.Add(new LineItemMaster
                        {
                            ItemName = item.ItemName,
                            Price = item.ItemRate,
                            InvoiceID = invoiceMaster.InvoiceID,
                            Qty = item.ItemQuantity,
                            Tax = item.ItemTax
                        });
                    }
                    var isLineItemsAdded = await _unitOfWork.LineItemMaster.AddRange(lineItemMasters);
                    if (isLineItemsAdded)
                    {
                        await _unitOfWork.SaveAsync();
                        return new ApiResponse
                        {
                            StatusCode = (int)StatusCode.Success,
                            Success = true,
                            Message = "Data Added Successfully"
                        };
                    }
                    else 
                    {
                        return new ApiResponse
                        {
                            StatusCode = (int)StatusCode.BadRequest,
                            Success = false,
                            Message = "Line Items not added"
                        }; 
                    }
                }
                else
                { 
                    return new ApiResponse
                    {
                        StatusCode = (int)StatusCode.BadRequest,
                        Success = false,
                        Message = "Data can not added in database"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = (int)StatusCode.InternalServerError,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
