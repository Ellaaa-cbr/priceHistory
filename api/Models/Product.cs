using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Product
    {
        public string Name { get; set; } = "";
        public int Id { get; set; } //manage id
        public int ProductNo { get; set; }

        public string category {get; set; } = "";
        
        public decimal price { get; set; }
        public DateTime date { get; set; }
        public string price_text { get; set; } = "";
        public int weight { get; set; }
        public string? Comment { get; set; }
        [Url]
        [MaxLength(2048)]
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

    }
}