using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _environment;

        public InvoiceController(IUnitOfWork unitOfWork, IInvoiceCreateRepository invoiceCreateRepository, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _invoiceCreateRepository = invoiceCreateRepository;
            _environment = environment;
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
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        [Route("upload-invoice")]
        public async Task<IActionResult> UploadInvoiceWithInvoiceID(int invoiceId, IFormFile uploadInvoice)
        {
            try
            {
                if (uploadInvoice == null || uploadInvoice.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Read the file data into a byte array
                byte[] photoData;
                using (var memoryStream = new MemoryStream())
                {
                    await uploadInvoice.CopyToAsync(memoryStream);
                    photoData = memoryStream.ToArray();
                }

                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

                // Check if the directory exists, and create it if it doesn't
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = Path.Combine(directoryPath, Path.GetRandomFileName());

                // Use FileStream to directly save the file
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await uploadInvoice.CopyToAsync(fileStream);
                }

                string fileUrl = GenerateFileUrl(fileName);

                // Save the photo data and ID to the database
                var photo = new InvoiceImage { InvoiceId = invoiceId, Image = photoData, ImagePath = fileUrl };
                var response = await _unitOfWork.InvoiceImage.Add(photo);
                await _unitOfWork.SaveAsync();

                return Ok(new { Id = photo.InvoiceId, FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        private string GenerateFileUrl(string filePath)
        {
            // Convert file path to a URI
            Uri fileUri = new Uri(filePath);

            // Get the absolute URL
            string fileUrl = fileUri.AbsoluteUri;

            return fileUrl;
        }

    }
}
