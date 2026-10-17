using KyNiem6Nam._02_Services._01_Interface;
using KyNiem6Nam._04_DOT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KyNiem6Nam._01_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var result = await _service.LoginAsync(request);

                if (result == null)
                {
                    return Unauthorized(new { message = "Mã PIN không chính xác!" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }

        [HttpGet("generate-hash")]
        public IActionResult GenerateHash(string pin)
        {
            // API này dùng để tạo nhanh chuỗi băm để bạn copy vào Database
            var hash = BCrypt.Net.BCrypt.HashPassword(pin);
            return Ok(new { MaPIN = pin, ChuoiHash = hash });
        }
    }
}
