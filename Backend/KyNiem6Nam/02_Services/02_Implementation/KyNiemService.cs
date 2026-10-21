using KyNiem6Nam._02_Services._01_Interface;
using KyNiem6Nam._03_Repositories._01_Interface;
using KyNiem6Nam._04_DOT.KyNiem;

namespace KyNiem6Nam._02_Services._02_Implementation
{
    public class KyNiemService : IKyNiemService
    {
        private readonly IKyNiemRepository _repo;

        public KyNiemService(IKyNiemRepository repo)
        {
            _repo = repo;
        }

        public async Task<DistanceResponseDTO> CalculateDistanceAsync()
        {
            var users = await _repo.GetAllUsersAsync();

            // Kiểm tra xem trong DB có đủ 2 người dùng không
            if (users == null || users.Count < 2)
            {
                return new DistanceResponseDTO { DistanceKm = 0, StatusMessage = "Chưa đủ dữ liệu vị trí" };
            }

            var user1 = users.FirstOrDefault(u => u.UserId == 1) ?? users[0];
            var user2 = users.FirstOrDefault(u => u.UserId == 2) ?? users[1];

            double lat1 = user1.CurrentLat ?? 0;
            double lon1 = user1.CurrentLng ?? 0;
            double lat2 = user2.CurrentLat ?? 0;
            double lon2 = user2.CurrentLng ?? 0;

            // Tính khoảng cách bằng công thức Haversine
            double distance = CalculateHaversine(lat1, lon1, lat2, lon2);
            distance = Math.Round(distance, 1); // Làm tròn 1 chữ số thập phân

            string status = distance <= 1.0 ? "0 km - Đang ở cạnh nhau" : $"{distance} km";

            return new DistanceResponseDTO
            {
                DistanceKm = distance,
                StatusMessage = status
            };
        }

        public async Task<UserSignalResponseDTO?> GetUserSignalAsync(int userId)
        {
            var user = await _repo.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserSignalResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                AvatarUrl = user.AvatarUrl,
                Battery = user.Pin != null ? $"{user.Pin}%" : "0%",
                Weather = user.ThoiTiet ?? "Đang cập nhật",
                Temperature = user.NhietDo != null ? $"{user.NhietDo}°C" : "0°C"
            };
        }

        // Công thức tính khoảng cách GPS thực tế (Haversine)
        private double CalculateHaversine(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Bán kính Trái Đất (km)
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
}