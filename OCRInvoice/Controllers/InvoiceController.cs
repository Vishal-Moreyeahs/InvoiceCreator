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
        private readonly IInvoiceCreateRepository _invoiceCreateRepository;
        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper, IInvoiceCreateRepository invoiceCreateRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _invoiceCreateRepository = invoiceCreateRepository;
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
            var response = await _invoiceCreateRepository.CreateInvoice(invoice);
            if (response)
            {
                return Ok(new { Status = true, Message = "Data Added Successully" });
            }
            else 
            {
                return Ok(new { Status = false, Message = "Data Not Processed" });
            }
        }
    }
}
