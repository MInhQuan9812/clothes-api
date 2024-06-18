using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class Category : Entity<int>
    {

        public string Name { get; set; }
        public string Thumbnail { get; set; }


        public virtual ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
