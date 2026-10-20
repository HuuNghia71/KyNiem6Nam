using KyNiem6Nam.Models;

namespace KyNiem6Nam._03_Repositories._01_Interface
{
    public interface IKyNiemRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync(); // Dùng để tính khoảng cách giữa 2 người
    }
}
