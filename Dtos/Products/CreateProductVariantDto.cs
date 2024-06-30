namespace clothes.api.Dtos.Products
{
    public class CreateProductVariantDto
    {
        public string VariantName { get; set; }
        public int Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public ICollection<CreateVarientValueDto> VarientValues { get; set; }

    }
}
