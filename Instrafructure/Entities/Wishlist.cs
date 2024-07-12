using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Wishlist : Entity<int>
    {
        public int? UserId { get; set; }       

        public virtual ICollection<WishlistItem> WishlistItems { get; set; }=new List<WishlistItem>();
        public virtual User User { get; set; }

        public Wishlist(int userId)
        {
            UserId = userId;
        }

        public Wishlist() { }
    }
}
