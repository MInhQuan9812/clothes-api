using clothes.api.Dtos.Payments;
using clothes.api.Dtos.Promotions;
using clothes.api.Dtos.User;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Dtos.Carts
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public int? Total { get; set; }
        public int? PromotionId { get; set; } = 0;
        public bool PaymentStatus { get; set; }
        public string PaymentUrl { get; set; }
        public int PaymentId { get; set; }
        public string Address { get; set; }
        public virtual UserDto Customer { get; set; }
        public virtual PaymentDto Payment { get; set; }
        public virtual PromotionDto Promotion { get; set; }

    }
}
