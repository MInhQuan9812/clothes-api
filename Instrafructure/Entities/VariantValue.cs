namespace clothes.api.Instrafructure.Entities
{
    public class VariantValue : Entity<int>
    {
        public int ProductId { get; set; }
        public int ProductVariantId { get; set; }
        public int OptionId { get; set; }
        public int OptionValueId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Option Option { get; set; }
        public virtual OptionValue OptionValue { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
