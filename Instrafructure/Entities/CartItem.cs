using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class CartItem : Entity<int>
    {
        public int CartId { get; set; }

        public int ProductVariantId { get; set; }

        public int Quantity { get; set; } = 0;

        public ProductVariant ProductVariant { get; set; }

        public virtual Cart Cart { get; set; }

        public CartItem(int productVariantId,int quantity) 
        {
            ProductVariantId= productVariantId;
            Quantity= quantity;
        }
    }
}
