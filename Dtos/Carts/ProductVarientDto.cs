using clothes.api.Dtos.Options;

namespace clothes.api.Dtos.Carts
{
    public class ProductVarientDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }
        public OptionValueDto OptionValue { get; set; }

        public ProductInfoDto ProductInfoDto { get; set; }

    }
}
