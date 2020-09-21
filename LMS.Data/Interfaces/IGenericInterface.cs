using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface IGenericInterface<TEntity> where TEntity:class
    {
        void Dispose();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> expression);
        void Update(TEntity entity);
        int Complete();
        Task<int> CompleteAsync(); 
    }
}
