using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Model
    {
        protected readonly ICosmosStore<TEntity> CosmosStore;

        public Repository(ICosmosStore<TEntity> cosmosStore)
        {
            CosmosStore = cosmosStore;
        }

        public async Task Add(TEntity obj)
        {
            await CosmosStore.AddAsync(obj);
        }

        public async Task Clear()
        {
            var all = await GetAll();
            await CosmosStore.RemoveRangeAsync(all);
        }

        public async Task<bool> Exists(Guid id)
        {
            var item = await GetById(id);
            return item != null;
        }

        public async Task<TEntity> FirstOrDefault()
        {
            return await CosmosStore.Query().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = CosmosStore.Query();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(string sql)
        {
            var query = CosmosStore.Query(sql);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await CosmosStore.FindAsync(id.ToString());
        }

        public async Task Remove(Guid id)
        {
            await CosmosStore.RemoveByIdAsync(id.ToString());
        }

        public async Task Update(TEntity obj)
        {
            await CosmosStore.UpdateAsync(obj);
        }
    }
}