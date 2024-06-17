using clothes.api.Instrafructure.Contant;
using System.ComponentModel.DataAnnotations;

namespace clothes.api.Instrafructure.Entities
{
    public class User : Entity<int>
    {

        public string UserName { get; set; }
        public string FullName { get; set; }

        [MaxLength(Contants.PhoneMaxLength)]
        public string? PhoneNumber { get; set; }

        [MaxLength(Contants.EmailMaxLength)]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Avatar { get; set; }

        public int Gender { get; set; } = Contants.Male;

        public string Role { get; set; } = Contants.Role;


        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public virtual Cart? Cart { get; set; }
    }
}
