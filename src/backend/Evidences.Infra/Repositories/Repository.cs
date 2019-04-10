// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="ArcTouch LLC">
//   Copyright 2019 ArcTouch LLC.
//   All rights reserved.
//
//   This file, its contents, concepts, methods, behavior, and operation
//   (collectively the "Software") are protected by trade secret, patent,
//   and copyright laws. The use of the Software is governed by a license
//   agreement. Disclosure of the Software to third parties, in any form,
//   in whole or in part, is expressly prohibited except as authorized by
//   the license agreement.
// </copyright>
// <summary>
//   Defines the Repository type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
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
        private readonly ICosmosStore<TEntity> _cosmosStore;

        public Repository(ICosmosStore<TEntity> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task Add(TEntity obj)
        {
            await _cosmosStore.AddAsync(obj);
        }

        public async Task<bool> Exists(Guid id)
        {
            var item = await GetById(id);
            return item != null;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _cosmosStore.Query();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _cosmosStore.FindAsync(id.ToString());
        }

        public async Task Remove(Guid id)
        {
            await _cosmosStore.RemoveByIdAsync(id.ToString());
        }

        public async Task Update(TEntity obj)
        {
            await _cosmosStore.UpdateAsync(obj);
        }
    }
}
