using KyNiem6Nam._04_DOT;
using KyNiem6Nam._04_DOT.Login;

namespace KyNiem6Nam._02_Services._01_Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}
