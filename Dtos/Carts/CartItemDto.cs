using clothes.api.Dtos.Options;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Carts
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; } 
        public ProductVarientDto ProductOptionValue { get; set; }
    }
}
