namespace clothes.api.Instrafructure.Entities
{
    public class WishlistItem : Entity<int>
    {
        public int WishlistId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public virtual Wishlist Wishlist { get; set; }

        public WishlistItem(int productId)
        {
            ProductId = productId;
        }

    }
}
