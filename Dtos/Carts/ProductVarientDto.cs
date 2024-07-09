using clothes.api.Dtos.Options;

namespace clothes.api.Dtos.Carts
{
    public class ProductVarientDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string VariantName { get; set; }
        public int Price {  get; set; }
        public ICollection<VariantValueDto> VariantValues { get; set; }

    }
}
