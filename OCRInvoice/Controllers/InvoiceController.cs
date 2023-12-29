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
        private readonly IInvoiceCreateRepository _invoiceCreateRepository;
        public InvoiceController(IUnitOfWork unitOfWork, IInvoiceCreateRepository invoiceCreateRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceCreateRepository = invoiceCreateRepository;
        }

        //[HttpPost]
        //[Route("addInvoice")]
        //public async Task<IActionResult> AddInvoice(InvoiceMaster invoiceMaster)
        //{
        //    var result = await _unitOfWork.InvoiceMaster.Add(invoiceMaster);
        //    await _unitOfWork.SaveAsync();
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("updateInvoiceToDb")]
        public async Task<IActionResult> UpdateInvoiceOcr(InvoiceOcrRequest invoice)
        {
            var response = await _invoiceCreateRepository.CreateInvoice(invoice);
            return Ok(response);
        }
    }
}
