using System.ComponentModel.DataAnnotations;

namespace clothes.api.Dtos.User
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; }
    }
}
