using Api_Compta.Models;
using Api_Compta.Models.Dtos;
using Api_Compta.Utils;
using Microsoft.EntityFrameworkCore;

namespace Api_Compta.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthService(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<AuthResponse?> LoginAsync(string login, string password)
        {
            var user = await _db.Users
                .SingleOrDefaultAsync(u => u.Login == login && u.IsActive == true);

            if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
                return null;

            if (user.Is2FAEnabled)
            {
                return new AuthResponse
                {
                    Requires2FA = true,
                    TempToken = _jwt.GenerateTempToken(user.UserId)
                };
            }

            return GenerateAuthResponse(user);
        }

        public AuthResponse GenerateAuthResponse(User user)
        {
            return new AuthResponse
            {
                Requires2FA = false,
                Token = _jwt.GenerateAccessToken(user.UserId, user.Login),
                RefreshToken = _jwt.GenerateRefreshToken()
            };
        }
    }

}
