using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Promotion : Entity<int>
    {


        public string Name { get; set; }
        


        public int Percentdiscount { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
