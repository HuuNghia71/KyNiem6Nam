using KyNiem6Nam._02_Services._01_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KyNiem6Nam._01_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class KyNiemController : ControllerBase
    {
        private readonly IKyNiemService _service;

        public KyNiemController(IKyNiemService service)
        {
            _service = service;
        }

        // 1. API tính khoảng cách giữa hai người
        [HttpGet("distance")]
        public async Task<IActionResult> GetDistance()
        {
            try
            {
                var result = await _service.CalculateDistanceAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }

        // 2, 3, 4. API lấy Pin, Thời tiết, Nhiệt độ theo ID từng người
        // Route này sẽ trả về gom chung các thông tin cá nhân theo userId
        [HttpGet("signal/{userId}")]
        public async Task<IActionResult> GetUserSignal(int userId)
        {
            try
            {
                var result = await _service.GetUserSignalAsync(userId);

                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy người dùng!" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }
    }
}
