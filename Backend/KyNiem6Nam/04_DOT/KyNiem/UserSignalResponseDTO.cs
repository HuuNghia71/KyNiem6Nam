namespace KyNiem6Nam._04_DOT.KyNiem
{
    public class UserSignalResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Battery { get; set; } = null!;      // VD: "89%"
        public string Weather { get; set; } = null!;      // VD: "Nắng nhẹ dịu dàng"
        public string Temperature { get; set; } = null!;  // VD: "28°C"
    }
}