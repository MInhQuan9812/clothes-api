namespace clothes.api.Instrafructure.Entities
{
    public class ProductVariantOption : Entity<int>
    {
        public int ProductVariantId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }
    }
}
