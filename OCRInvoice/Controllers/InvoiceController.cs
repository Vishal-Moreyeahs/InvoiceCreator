using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCRInvoice.Entities;
using OCRInvoice.Interfaces;
using OCRInvoice.Models.Request;

namespace OCRInvoice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("addInvoice")]
        public async Task<IActionResult> AddInvoice(InvoiceMaster invoiceMaster)
        {
            var result = await _unitOfWork.InvoiceMaster.Add(invoiceMaster);
            await _unitOfWork.SaveAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("updateInvoiceToDb")]
        public async Task<IActionResult> UpdateInvoiceOcr(InvoiceOcrRequest invoice)
        {
            var data = invoice.Worksheet1.FirstOrDefault();
            var customer = new Customer
            { 
                Name = data.ReceiverName,
                Address = data.ReceiverAddress,
                TaxId = data.ReceiverGSTNumber                
            };
            var invoiceMaster =new InvoiceMaster
            { 
                InvoiceNumber = data.InvoiceNumber,
                Date = DateTime.Parse(data.InvoiceDate),
                Address = data.SenderAddress,
                ProviderName = data.SenderName,
                TaxId= data.SenderGSTNumber,
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
                    var isLineItemsAdded =await _unitOfWork.LineItemMaster.Add(lineItem);
                    if (isLineItemsAdded)
                    {
                        await _unitOfWork.SaveAsync();
                    }
                }
                return Ok(new { Message = "Data Added Successfully" });
            }
            return Ok(new { Message = "Data Not Processed"});
        }
    }
}
