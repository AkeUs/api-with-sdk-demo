using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Api.Sdk.Models;

namespace Todo.Api.Sdk.Contracts {
    public interface ITodoClient {
        Task<IEnumerable<TodoItem>> GetTodos();
        Task<TodoItem> GetTodoById(int id);
        Task<TodoItem> CreateTodo(string description);
        Task<bool> UpdateTodo(TodoItem todo);
        Task<bool> DeleteTodo(TodoItem todo);
    }
}
