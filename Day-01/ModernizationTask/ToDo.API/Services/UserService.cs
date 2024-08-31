using System.Security.Cryptography;
using System.Text;
using ToDo.API.Exceptions;
using ToDo.API.Interfaces.Repositories;
using ToDo.API.Interfaces.Services;
using ToDo.API.Models.DBModels;
using ToDo.API.Models.DTOs;

namespace ToDo.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<TokenDTO> Login(LoginDTO loginDTO)
        {
            User? user = await _userRepo.GetByUsername(loginDTO.Username);

            if (user == null)
                throw new EntityNotFoundException("User does not exist.");

            if (!IsPasswordCorrect(loginDTO.Password, user.PasswordHashKey, user.HashedPassword))
                throw new InvalidCredentialsException();

            return new TokenDTO
            {
                AccessToken = _tokenService.GenerateToken(user),
                TokenType = "Bearer",
            };
        }

        public async Task<RegistrationResultDTO> Register(RegisterDTO registerDTO)
        {
            User? existingUser = await _userRepo.GetByUsername(registerDTO.Username);
            if (existingUser != null)
                throw new UsernameTakenException();

            if (!IsPasswordStrong(registerDTO.Password))
                throw new WeakPasswordException();

            User user = new User
            {
                Username = registerDTO.Username,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
            };

            using var hmac = new HMACSHA512();
            user.PasswordHashKey = hmac.Key;
            user.HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));

            await _userRepo.Add(user);

            return new RegistrationResultDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        private bool IsPasswordCorrect(string password, byte[] passwordHashKey, byte[] storedPasswordHash)
        {
            using var hmac = new HMACSHA512(passwordHashKey);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedPasswordHash);
        }

        private bool IsPasswordStrong(string password)
        {
            const int minLength = 8;
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return password.Length >= minLength && hasUpperCase && hasLowerCase && hasDigit;
        }
    }
}
