﻿using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Product
{
    public class ProductDto 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string? Thumbnail { get; set; }

        public virtual ICollection<ProductOptionValueResponeDto> ProductOptionValues { get; set; } = new List<ProductOptionValueResponeDto>();
    }
}
