using ToDo.API.Models.DTOs;
namespace ToDo.API.Interfaces.Services
{
    public interface ITodoService
    {
        public Task<TodoResponseDTO> CreateTodo(int userId, TodoDTO todo);
        public Task<TodoResponseDTO> GetTodoById(int userId, int todoId);
        public Task<IEnumerable<TodoResponseDTO>> GetTodos(int userId);
        public Task<TodoResponseDTO> UpdateTodo(int userId, int todoId, TodoDTO todo);
        public Task<bool> DeleteTodoById(int userId, int todoId);
    }
}
