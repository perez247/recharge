using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Single Entity
         Task<TEntity> GetById(int id);
         Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        // Multiple Entity
         Task<IEnumerable<TEntity>> GetAll();
         IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // Add Entity
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entity);

        // Remove Entity
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);

    }
}