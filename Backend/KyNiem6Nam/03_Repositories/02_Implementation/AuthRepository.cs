using KyNiem6Nam._03_Repositories._01_Interface;
using KyNiem6Nam.Models;
using Microsoft.EntityFrameworkCore;

namespace KyNiem6Nam._03_Repositories._02_Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly KyNiem6NamContext _context;

        public AuthRepository(KyNiem6NamContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
