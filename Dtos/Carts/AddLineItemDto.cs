namespace clothes.api.Dtos.Carts
{
    public class AddLineItemDto
    {
        public int ProductVarientId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
