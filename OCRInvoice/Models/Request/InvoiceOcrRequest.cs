using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OCRInvoice.Models.Request
{
    public class InvoiceOcrRequest
    {
        public List<Worksheet1Item> Worksheet1 { get; set; }
        public List<Worksheet2Item> Worksheet2 { get; set; }
    }

    public class Worksheet1Item
    {
        [JsonPropertyName("sender_name")]
        [DefaultValue("M/s. Chintamani Construction")]
        public string? SenderName { get; set; }

        [JsonPropertyName("sender_address")]
        [DefaultValue("100,Manorma Colony, Near Shankar Mandir,Sagar,MP-470001")]
        public string? SenderAddress { get; set; }

        [JsonPropertyName("sender_gst_number")]
        [DefaultValue("23ADVPT081481ZP")]
        public string? SenderGSTNumber { get; set; }

        [JsonPropertyName("receiver_name")]
        [DefaultValue("M/s. Shreeji Infrastucture india PVT. Ltd.")]
        public string? ReceiverName { get; set; }

        [JsonPropertyName("receiver_address")]
        [DefaultValue("Head Office - A4, Emerald Park City, Near AIMS, Bagh Sewania, Bhopal - 462043 Madhya Pradesh (India)")]
        public string? ReceiverAddress { get; set; }

        [JsonPropertyName("receiver_gst_number")]
        [DefaultValue("23AAGCS7896M1ZH")]
        public string? ReceiverGSTNumber { get; set; }

        [JsonPropertyName("invoice_number")]
        [DefaultValue("INV-0000001")]
        public string? InvoiceNumber { get; set; }

        [JsonPropertyName("invoice_date")]
        [DefaultValue("02.10.2023")]
        public string? InvoiceDate { get; set; }

        [JsonPropertyName("invoice_category")]
        [DefaultValue("tax invoice")]
        public string? InvoiceCategory { get; set; }

        [JsonPropertyName("total_amount")]
        [DefaultValue("705714.08")]
        public double? TotalAmount { get; set; }

        [JsonPropertyName("clarity_percentage")]
        [DefaultValue("4.08120484758166")]
        public string? OcrPercentage { get; set; }
    }

    public class Worksheet2Item
    {
        [JsonPropertyName("item_name")]
        [DefaultValue("Four laning of Nanasa to Pidgaon section of NH-47 (Old NH-59A) (indore-Harda, Pkg-ill) (Length-47,445 km) Design Ch. 95+0( Km to 142+445 km under Bharatmaia Pariyojana Phase-| (Economic Corridor) in the State of Madhya Pradesh on Hybrid Annu Mode")]
        public string? ItemName { get; set; }

        [JsonPropertyName("item_quantity")]
        [DefaultValue(1)]
        public int? ItemQuantity { get; set; }

        [JsonPropertyName("item_rate")]
        [DefaultValue(705714.08)]
        public double? ItemRate { get; set; }

        [JsonPropertyName("item_tax")]
        [DefaultValue(0)]
        public double? ItemTax { get; set; }

        [JsonPropertyName("item_amount")]
        [DefaultValue(705714.08)]
        public double? ItemAmount { get; set; }
    }

}
