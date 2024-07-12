using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string? Thumbnail { get; set; }

        //public virtual OrderDetail OrderDetail { get; set; }
        public virtual Category Category { get; set; }
        public virtual WishlistItem WishlistItem { get; set; }
        public virtual ICollection<OptionValue> OptionValues { get; set; }=new List<OptionValue>();
        public virtual ICollection<ProductOptionValue> ProductOptionValues { get; set; } = new List<ProductOptionValue>();
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } =new List<ProductVariant>();
        public virtual ICollection<VariantValue> VariantValues { get; set; }=new List<VariantValue>();  
    }
}
