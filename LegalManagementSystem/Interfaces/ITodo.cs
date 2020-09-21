using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ITodo
    {
        void AddTodo(Todo todo);
        int Complete();
        Task<int> CompleteAsync();
        void DeleteTodo(Todo todo);
        void Dispose();
        Todo GetTodo(int? id);
        IEnumerable<Todo> GetTodos();
        void UpdateTodo(Todo todo);
        IEnumerable<Todo> GetTodos(Expression<Func<Todo, bool>> expression);
        Task<IEnumerable<Todo>> GetTodosAsync();
        Task<IEnumerable<Todo>> GetTodosAsync(Expression<Func<Todo, bool>> expression);
        Task<Todo> GetTodoAsync(Guid? id);
        Todo GetTodo(Func<Todo, bool> p);
    }
}
