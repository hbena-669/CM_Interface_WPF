using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api_Compta.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(Guid userId, string login)
        {
            var claims = new[]
            {
                new Claim("uid", userId.ToString()),   // interne, clair, stable
                new Claim("login", login)
            };

            return GenerateToken(claims,
                TimeSpan.FromMinutes(_config.GetValue<int>("Jwt:AccessTokenMinutes")));
        }

        public string GenerateTempToken(Guid userId)
        {
            var claims = new[]
            {
                new Claim("uid", userId.ToString()),
                new Claim("type", "2fa")
            };

            return GenerateToken(claims, TimeSpan.FromMinutes(5));
        }

        private string GenerateToken(IEnumerable<Claim> claims, TimeSpan duration)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(duration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

}
