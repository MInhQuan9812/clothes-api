namespace clothes.api.Instrafructure.Entities
{
    public class ProductVariant : Entity<int>
    {
        public string VariantName { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public ICollection<Sku> Skus { get; set; }  =new List<Sku>();
        public ICollection<VariantValue> VariantValues { get; set; }=new List<VariantValue>();
    }
}
