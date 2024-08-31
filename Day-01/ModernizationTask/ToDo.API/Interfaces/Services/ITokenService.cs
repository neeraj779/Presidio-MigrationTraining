using ToDo.API.Models.DBModels;

namespace ToDo.API.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
