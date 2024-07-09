using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Dtos.Products;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Product
{
    public class ProductDto 
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string? Thumbnail { get; set; }

        public virtual ICollection<ProductSkuDto> ProductVariants { get; set; } = new List<ProductSkuDto>();
        //public virtual ICollection<OptionDto> ProductOptionValues { get; set; } = new List<OptionDto>();
    }
}
