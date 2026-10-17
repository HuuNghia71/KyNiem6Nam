using KyNiem6Nam._02_Services._01_Interface;
using KyNiem6Nam._03_Repositories._01_Interface;
using KyNiem6Nam._04_DOT;
using KyNiem6Nam._04_DOT.Login;
using KyNiem6Nam.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KyNiem6Nam._02_Services._02_Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
        {
            // 1. Lấy toàn bộ User (2 người)
            var users = await _repo.GetAllUsersAsync();

            User? loggedInUser = null;

            // 2. Dùng BCrypt để so sánh mã PIN thô với chuỗi Hash trong Database
            foreach (var user in users)
            {
                try
                {
                    if (BCrypt.Net.BCrypt.Verify(request.Passcode, user.Passcode))
                    {
                        loggedInUser = user;
                        break;
                    }
                }
                catch (Exception)
                {
                    // Bỏ qua nếu dòng data này trong SQL bị lỗi (không phải mã BCrypt)
                    continue;
                }
            }

            // Nếu quét qua hết mà không ai khớp -> Sai mã PIN
            if (loggedInUser == null) return null;

            // 3. Tạo Token JWT (Giữ nguyên như lúc trước)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserId.ToString()),
                    new Claim(ClaimTypes.Name, loggedInUser.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponseDTO
            {
                UserId = loggedInUser.UserId,
                Username = loggedInUser.Username,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}