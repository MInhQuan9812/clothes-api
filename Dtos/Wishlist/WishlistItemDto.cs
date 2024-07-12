using clothes.api.Dtos.Carts;
using clothes.api.Dtos.Product;

namespace clothes.api.Dtos.Wishlist
{
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public ProductDto Product { get; set; }
    }
}
