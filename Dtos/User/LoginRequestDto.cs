using System.ComponentModel.DataAnnotations;

namespace clothes.api.Dtos.User
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
