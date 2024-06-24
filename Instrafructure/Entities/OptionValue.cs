namespace clothes.api.Instrafructure.Entities
{
    public class OptionValue : Entity<int>
    { 

        public int OptionId { get; set; }
        public string Value { get; set; }
        public int Price { get; set; } = 0;
        public string? Thumbnail { get; set; }   
        public virtual Option Option { get; set; }
        public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }
    }
}
