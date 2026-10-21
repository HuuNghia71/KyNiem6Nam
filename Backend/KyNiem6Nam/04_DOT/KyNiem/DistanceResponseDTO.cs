namespace KyNiem6Nam._04_DOT.KyNiem
{
    public class DistanceResponseDTO
    {
        public double DistanceKm { get; set; }
        public string StatusMessage { get; set; } = null!; // VD: "0 km - Đang ở cạnh nhau"
    }
}
