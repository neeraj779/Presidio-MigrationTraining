using ToDo.API.Exceptions;
using ToDo.API.Interfaces.Repositories;
using ToDo.API.Interfaces.Services;
using ToDo.API.Models.DBModels;
using ToDo.API.Models.DTOs;

namespace ToDo.API.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<int, Todo> _todoRepository;
        private readonly IUserRepository _userRepository;

        public TodoService(IRepository<int, Todo> todoRepository, IUserRepository userRepository)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
        }

        public async Task<TodoResponseDTO> CreateTodo(int userId, TodoDTO todoDto)
        {
            User user = await ValidateUserExists(userId);

            var newTodo = new Todo
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                UserId = userId
            };

            await _todoRepository.Add(newTodo);

            return MapToResponseDto(newTodo, user);
        }

        public async Task<TodoResponseDTO> GetTodoById(int userId, int todoId)
        {
            User user = await ValidateUserExists(userId);

            var todo = await _todoRepository.GetById(todoId)
                        ?? throw new EntityNotFoundException("Todo does not exist.");

            return MapToResponseDto(todo, user);
        }

        public async Task<IEnumerable<TodoResponseDTO>> GetTodos(int userId)
        {
            User user = await ValidateUserExists(userId);

            var todos = await _todoRepository.GetAll();

            return todos
                .Where(todo => todo.UserId == userId)
                .Select(todo => MapToResponseDto(todo, user));
        }

        public async Task<TodoResponseDTO> UpdateTodo(int userId, int todoId, TodoDTO todoDto)
        {
            User user = await ValidateUserExists(userId);

            var existingTodo = await _todoRepository.GetById(todoId)
                            ?? throw new EntityNotFoundException("Todo does not exist.");

            existingTodo.Title = todoDto.Title;
            existingTodo.Description = todoDto.Description;

            await _todoRepository.Update(existingTodo);

            return MapToResponseDto(existingTodo, user);
        }

        public async Task<bool> DeleteTodoById(int userId, int todoId)
        {
            User user = await ValidateUserExists(userId);

            var todo = await _todoRepository.GetById(todoId)
                        ?? throw new EntityNotFoundException("Todo does not exist.");

            await _todoRepository.Delete(todoId);

            return true;
        }

        private async Task<User> ValidateUserExists(int userId)
        {
            User user = await _userRepository.GetById(userId);
            if (user == null)
                throw new EntityNotFoundException("User does not exist.");
            return user;
        }

        private TodoResponseDTO MapToResponseDto(Todo todo, User user)
        {
            return new TodoResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                TodoId = todo.TodoId,
                Title = todo.Title,
                Description = todo.Description,
                TargetDate = todo.TargetDate,
                Status = todo.Status
            };
        }
    }
}
