using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class CartItem : Entity<int>
    {
        public int CartId { get; set; }

        public int ProductOptionValueId { get; set; }

        public int Quantity { get; set; } = 0;

        public ProductOptionValue ProductOptionValue { get; set; }

        public virtual Cart Cart { get; set; }

        public CartItem(int productOptionValueId,int quantity) 
        {
            ProductOptionValueId= productOptionValueId;
            Quantity= quantity;
        }
    }
}
