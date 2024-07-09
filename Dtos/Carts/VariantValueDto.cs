namespace clothes.api.Dtos.Carts
{
    public class VariantValueDto
    {
        public int ProductVariantId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }

        public OptionValueNameFieldDto OptionValue { get; set; }
    }
}
