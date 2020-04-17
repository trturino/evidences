using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evidences.Domain.Models;

namespace Evidences.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Model
    {
        Task Add(TEntity obj);

        Task Update(TEntity obj);

        Task Remove(Guid id);

        Task<TEntity> GetById(Guid id);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<IEnumerable<TEntity>> GetAll(string sql);

        Task<bool> Exists(Guid id);

        Task<TEntity> FirstOrDefault();

        Task Clear();
    }
}