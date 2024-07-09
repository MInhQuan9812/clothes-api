using clothes.api.Instrafructure.DesignPattern.Promotion;
using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Order : Entity<int>, IOrder
    {

        public int CustomerId { get; set; }
        public DateTime Created { get; set; }
        public string Address { get; set; }
        public int PaymentId { get; set; }

        public int? Total { get; set; }
        public int? PromotionId { get; set; } = 0;


        public virtual User Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual Promotion Promotion { get; set; }

        public virtual Payment Payment { get; set; }

        public int? GetTotalPrice => Total;

    }
}

