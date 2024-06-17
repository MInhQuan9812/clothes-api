namespace clothes.api.Dtos.User
{
    public class UpdateInfoDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }


        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public int Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string? Avatar { get; set; }
    }
}
