namespace clothes.api.Instrafructure.Entities
{
    public class Option : Entity<int>
    {

        public string Name { get; set; }

        public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; }
    }
}
