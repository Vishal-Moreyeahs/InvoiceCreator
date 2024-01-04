using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OCRInvoice.Entities
{
    public class InvoiceImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int? InvoiceId { get; set; }
        public byte[]? Image { get; set; }
    }
}
