// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="ArcTouch LLC">
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
//   Defines the IRepository type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
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

    }
}
