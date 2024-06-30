using clothes.api.Dtos.Options;

namespace clothes.api.Dtos.Products
{
    public class ProductOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<ProductOptionValueDto> OptionValues { get; set; } = new List<ProductOptionValueDto>();

    }
}
