using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Wishlist : Entity<int>
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        
        //ProductId
        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
    }
}
