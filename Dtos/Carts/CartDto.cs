using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }

        public DateTime? CreateAt { get; set; } = DateTime.Now;

        public DateTime? LastUpdate { get; set; } = DateTime.Now;

        public virtual ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

    }
}
