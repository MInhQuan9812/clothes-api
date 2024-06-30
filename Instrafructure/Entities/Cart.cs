using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Cart : Entity<int>
    {

        public int? CustomerId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual User? Customer { get; set; }

        public Cart(int customerId)
        {
            CustomerId = customerId;
        }

        public Cart() { }
    }
}
