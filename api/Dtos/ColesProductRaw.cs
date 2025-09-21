using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class ColesProductRaw
    {
        public string? url { get; set; }
        public string? name { get; set; }
        public string? price_text { get; set; }
        public decimal? price_value { get; set; }
        public string? image_url { get; set; }
        public string? category_path { get; set; }
    }
}