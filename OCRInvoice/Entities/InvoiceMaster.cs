﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OCRInvoice.Entities
{
    public class InvoiceMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? ProviderName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? Date { get; set; }
        public string? TaxId { get; set; }
        public double? Total { get; set; }
        public string? Tax1 { get; set; }
        public string? Tax2 { get; set; }
        public string? Tax3 { get; set; }
        public string? ImageUrl { get; set; }
        public string? OcrPercentage { get; set; }
    }
}
