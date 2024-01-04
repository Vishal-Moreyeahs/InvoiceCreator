﻿namespace OCRInvoice.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
