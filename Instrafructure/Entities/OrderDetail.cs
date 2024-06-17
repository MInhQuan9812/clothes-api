using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    [Index(nameof(OrderId), IsUnique = false)]
    public class OrderDetail : Entity<int>
    {
        //Mã
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }


        public virtual Order Order { get; set; }
        public Product Product { get; set; }

    }
}
