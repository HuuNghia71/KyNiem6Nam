using KyNiem6Nam.Models;

namespace KyNiem6Nam._03_Repositories._01_Interface
{
    public interface IAuthRepository
    {
        Task<List<User>> GetAllUsersAsync();
    }
}
