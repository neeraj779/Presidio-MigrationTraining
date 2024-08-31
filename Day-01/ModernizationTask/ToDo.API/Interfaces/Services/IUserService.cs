using ToDo.API.Models.DTOs;

namespace ToDo.API.Interfaces.Services
{
    public interface IUserService
    {
        public Task<TokenDTO> Login(LoginDTO user);
        public Task<RegistrationResultDTO> Register(RegisterDTO user);
    }
}
