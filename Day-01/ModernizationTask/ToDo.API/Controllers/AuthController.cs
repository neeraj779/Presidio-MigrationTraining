using Microsoft.AspNetCore.Mvc;
using ToDo.API.Exceptions;
using ToDo.API.Interfaces.Services;
using ToDo.API.Models;
using ToDo.API.Models.DTOs;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                TokenDTO token = await _userService.Login(loginDTO);
                return Ok(token);
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new ErrorModel { Status = StatusCodes.Status401Unauthorized, Message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ErrorModel { Status = StatusCodes.Status404NotFound, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                RegistrationResultDTO result = await _userService.Register(registerDTO);
                return Ok(result);
            }
            catch (WeakPasswordException ex)
            {
                return BadRequest(new ErrorModel { Status = StatusCodes.Status400BadRequest, Message = ex.Message });
            }
            catch (UsernameTakenException ex)
            {
                return Conflict(new ErrorModel { Status = StatusCodes.Status409Conflict, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
