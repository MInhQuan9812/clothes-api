using System.ComponentModel.DataAnnotations;

namespace clothes.api.Dtos.User
{
    public class SignUpDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }



    }
}
