using ToDo.API.Contexts;
using ToDo.API.Models.DBModels;

namespace ToDo.API.Repositories
{
    public class TodoRepository : AbstractRepository<int, Todo>
    {
        public TodoRepository(ToDoContext context) : base(context)
        {
        }
    }
}
