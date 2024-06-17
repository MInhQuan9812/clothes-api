namespace clothes.api.Dtos.User
{
    public class LoginResponeDto
    {
        public string Token { get; set; }
        public UserDto User { get;set; }

        public LoginResponeDto(string token,UserDto userDto) 
        {
            Token = token;
            User = userDto;
        }

    }
}
