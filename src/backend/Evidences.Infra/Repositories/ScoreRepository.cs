// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreRepository.cs" company="ArcTouch LLC">
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
//   Defines the ScoreRepository type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using Cosmonaut;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Infra.Repositories
{
    public class ScoreRepository : Repository<Score>, IScoreRepository
    {
        public ScoreRepository(ICosmosStore<Score> cosmosStore) : base(cosmosStore)
        {
        }
    }
}
