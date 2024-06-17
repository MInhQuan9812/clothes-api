using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Category : Entity<int>
    {

        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
