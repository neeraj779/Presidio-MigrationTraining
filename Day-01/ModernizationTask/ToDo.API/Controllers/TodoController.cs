using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.API.Exceptions;
using ToDo.API.Interfaces.Services;
using ToDo.API.Models;
using ToDo.API.Models.DTOs;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<ActionResult<TodoResponseDTO>> CreateTodo([FromBody] TodoDTO todoDto)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todo = await _todoService.CreateTodo(userId, todoDto);
                return Ok(todo);
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

        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoResponseDTO>> GetTodoById(int todoId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todo = await _todoService.GetTodoById(userId, todoId);
                return Ok(todo);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoResponseDTO>>> GetTodos()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todos = await _todoService.GetTodos(userId);
                return Ok(todos);
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

        [HttpPut("{todoId}")]
        public async Task<ActionResult<TodoResponseDTO>> UpdateTodo(int todoId, [FromBody] TodoDTO todoDto)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var todo = await _todoService.UpdateTodo(userId, todoId, todoDto);
                return Ok(todo);
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

        [HttpDelete("{todoId}")]
        public async Task<ActionResult> DeleteTodoById(int todoId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                bool res = await _todoService.DeleteTodoById(userId, todoId);
                return Ok(res);
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
    }
}
