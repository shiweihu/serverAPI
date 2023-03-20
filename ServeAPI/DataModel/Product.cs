using System;
namespace ServeAPI.DataModel
{
    public class Product
    {
      
        public int? id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;
        public string image_url { get; set; } = string.Empty;
        public decimal price { get; set; }
    }
}

