using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string? Thumbnail { get; set; }

        //Option
        //public ICollection<CreateOptionDto> Options { get; set; } =new List<CreateOptionDto>();
        // OptionValue
        public ICollection<CreateOptionValueDto> OptionValues { get; set; } =new List<CreateOptionValueDto>();

        public ICollection<CreateProductVariantDto> ProductVariants { get; set; } = new List<CreateProductVariantDto>();
        //public virtual OrderDetail OrderDetail { get; set; }
    }
}
