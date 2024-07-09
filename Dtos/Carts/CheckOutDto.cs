namespace clothes.api.Dtos.Carts
{
    public class CheckOutDto
    {
        public string Address { get; set; }
        public int Total {  get; set; }
        public int PromotionId { get; set; }
        public int PaymentId { get; set; }
    }
}
