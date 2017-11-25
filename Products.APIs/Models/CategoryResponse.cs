﻿
namespace Products.APIs.Models
{
    using System.Collections.Generic;
    public class CategoryResponse
    {
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public List<ProductResponse> Products{ get; set; }
    }
}