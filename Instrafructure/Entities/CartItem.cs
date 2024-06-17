using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class CartItem : Entity<int>
    {


        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 0;

        public Product Product { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
