namespace clothes.api.Instrafructure.Entities
{
    public class OptionValue : Entity<int>
    {
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        public string? Thumbnail { get; set; }

        public virtual Product Product { get; set; }
        public virtual Option Option { get; set; }
        public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }
        public virtual ICollection<VariantValue> VarientValues { get; set; } = new List<VariantValue>();

    }
}
