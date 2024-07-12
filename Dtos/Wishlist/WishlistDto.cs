using clothes.api.Dtos.Carts;
using clothes.api.Dtos.Product;

namespace clothes.api.Dtos.Wishlist
{
    public class WishlistDto
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreateAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdate { get; set; } = DateTime.Now;

        public virtual ICollection<WishlistItemDto> WishlistItems { get; set; } = new List<WishlistItemDto>();
    }
}
