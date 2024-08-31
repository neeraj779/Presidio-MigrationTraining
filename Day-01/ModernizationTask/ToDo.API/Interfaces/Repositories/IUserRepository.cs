using ToDo.API.Models.DBModels;

namespace ToDo.API.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<int, User>
    {
        public Task<User?> GetByUsername(string username);
    }
}
