using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Promotion : Entity<int>
    {
        public string Name { get; set; }
        public int PromotionTypeId { get; set; }
        public int PromotionValue { get; set; }

        public virtual PromotionType PromotionType { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
