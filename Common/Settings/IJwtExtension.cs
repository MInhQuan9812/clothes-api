using System.Security.Claims;

namespace clothes.api.Common.Settings
{
    public interface IJwtExtension
    {
        string GenerateToken(int id, string role);

        IEnumerable<Claim> DecodeToken(string token);
    }
}
