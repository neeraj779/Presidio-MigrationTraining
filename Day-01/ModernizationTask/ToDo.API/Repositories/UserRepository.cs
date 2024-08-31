using Microsoft.EntityFrameworkCore;
using ToDo.API.Contexts;
using ToDo.API.Interfaces.Repositories;
using ToDo.API.Models.DBModels;

namespace ToDo.API.Repositories
{
    public class UserRepository : AbstractRepository<int, User>, IUserRepository
    {
        public UserRepository(ToDoContext context) : base(context)
        {
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
