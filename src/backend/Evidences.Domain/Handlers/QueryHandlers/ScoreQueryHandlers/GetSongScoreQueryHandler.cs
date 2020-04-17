// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetSongScoreQueryHandler.cs" company="ArcTouch LLC">
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
//   Defines the GetSongScoreQueryHandler type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Queries.ScoreQueries;
using Evidences.Domain.Repositories;
using System.Linq;
namespace Evidences.Domain.Handlers.QueryHandlers.ScoreQueryHandlers
{
    public class GetSongScoreQueryHandler : ICommandHandler<GetSongScoreQuery, int>
    {
        private readonly IScoreRepository _scoreRepository;

        public GetSongScoreQueryHandler(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public async Task<int> ExecuteAsync(GetSongScoreQuery command, int previousResult)
        {
            var score = await _scoreRepository.GetAll($"select * from c where c.songId = '{command.SongId}'");

            if (score.Count() == 0)
            {
                return 0;
            }

            return score.Sum(x => x.ScoreNumber) / score.Count();
        }
    }
}
