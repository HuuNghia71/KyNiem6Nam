using KyNiem6Nam._04_DOT.KyNiem;

namespace KyNiem6Nam._02_Services._01_Interface
{
    public interface IKyNiemService
    {
        Task<DistanceResponseDTO> CalculateDistanceAsync();
        Task<UserSignalResponseDTO?> GetUserSignalAsync(int userId);
    }
}
