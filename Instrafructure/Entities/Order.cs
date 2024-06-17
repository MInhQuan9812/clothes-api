using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Order : Entity<int>
    {

        public int CustomerId { get; set; }
        public DateTime Created { get; set; }
        public string Address { get; set; }
        public int PaymentId { get; set; }

        public long? Total { get; set; }
        public int? PromotionId { get; set; } = 0;


        public virtual User Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual Promotion Promotion { get; set; }

        public virtual Payment Payment { get; set; }

        public long? GetTotalPrice => Total;

    }
}

