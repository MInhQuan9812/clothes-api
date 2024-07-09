using System.ComponentModel.DataAnnotations;

namespace clothes.api.Dtos.User
{
    public class ResetPasswordDto
    {
        public int Id { get; set; }

        [Required]
        public string OldPassword { get;set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
