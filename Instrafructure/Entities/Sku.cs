namespace clothes.api.Instrafructure.Entities
{
    public class Sku : Entity<int>
    {
        public int ProductVariantId { get; set; }    
        public string SkuCode {  get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
