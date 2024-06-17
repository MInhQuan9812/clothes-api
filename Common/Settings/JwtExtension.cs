using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clothes.api.Common.Settings
{
    public class JwtExtension : IJwtExtension
    {
        private readonly JwtSetting _jwtSetting;

        public JwtExtension(IOptions<JwtSetting> jwtSettingOption)
        {
            _jwtSetting = jwtSettingOption.Value;
        }


        public IEnumerable<Claim> DecodeToken(string token)
        {
            var obj = new JwtSecurityToken(token);
            return obj.Claims;
        }

        public string GenerateToken(int id, string role)
        {
            
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)),SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id",id.ToString()),
                    new Claim(ClaimTypes.Role,id.ToString())
                }),
                Expires=DateTime.UtcNow.AddDays(_jwtSetting.ExpiryDays),
                SigningCredentials=signingCredentials,
                Audience=_jwtSetting.ValidIssuer,
                Issuer=_jwtSetting.ValidIssuer,
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
        }
    }
}
