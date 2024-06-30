namespace clothes.api.Instrafructure.Entities
{
    public class Option : Entity<int>
    {

        public string Name { get; set; }
        public virtual ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();
        public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }
        public virtual ICollection<VariantValue> VarientValues { get; set; }=new List<VariantValue>();
    }
}
