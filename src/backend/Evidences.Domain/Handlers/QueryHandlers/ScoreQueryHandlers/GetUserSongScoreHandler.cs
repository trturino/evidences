// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetUserSongScoreHandler.cs" company="ArcTouch LLC">
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
//   Defines the GetUserSongScoreHandler type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Queries.ScoreQueries;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.QueryHandlers.ScoreQueryHandlers
{
    public class GetUserSongScoreHandler : ICommandHandler<GetUserSongScoreQuery, int>
    {
        private readonly IScoreRepository scoreRepository;

        public GetUserSongScoreHandler(IScoreRepository scoreRepository)
        {
            this.scoreRepository = scoreRepository;
        }

        public Task<int> ExecuteAsync(GetUserSongScoreQuery command, int previousResult)
        {
            throw new System.NotImplementedException();
        }
    }
}
