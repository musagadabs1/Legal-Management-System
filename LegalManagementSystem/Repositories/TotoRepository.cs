using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class TotoRepository : ITodo
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddTodo(Todo todo)
        {
            try
            {
                db.Todoes.Add(todo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Complete()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteTodo(Todo todo)
        {
            try
            {
                db.Todoes.Remove(todo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Todo GetTodo(int? id)
        {
            try
            {
                return db.Todoes.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Todo GetTodo(Expression<Func<Todo, bool>> expression)
        {
            try
            {
                return db.Todoes.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Todo> GetTodoAsync(int? id)
        {
            try
            {
                return await db.Todoes.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Todo> GetTodoAsync(Guid? id)
        {
            try
            {
                return await db.Todoes.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Todo GetTodo(Func<Todo, bool> p)
        {
            try
            {
                return db.Todoes.Where(p).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Todo> GetTodos()
        {
            try
            {
                return db.Todoes.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Todo> GetTodos(Expression<Func<Todo, bool>> expression)
        {
            try
            {
                return db.Todoes.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync()
        {
            try
            {
                return await db.Todoes.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(Expression<Func<Todo, bool>> expression)
        {
            try
            {
                return await db.Todoes.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateTodo(Todo todo)
        {
            try
            {
                db.Entry(todo).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}