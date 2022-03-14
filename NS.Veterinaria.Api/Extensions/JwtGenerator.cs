using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NS.Veterinary.Api.Extensions
{
    public static class JwtGenerator
    {
        public static string GenerateJwt(JwtSettings jwtSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encondedToke = tokenHandler.WriteToken(token);
            return encondedToke;
        }
    }
}
