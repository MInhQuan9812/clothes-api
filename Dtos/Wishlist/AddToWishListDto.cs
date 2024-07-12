using System.ComponentModel.DataAnnotations;

namespace clothes.api.Dtos.Wishlist
{
    public class AddToWishListDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}
