// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreService.cs" company="ArcTouch LLC">
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
//   Defines the ScoreService type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using Evidences.Repositories;
using Evidences.Models;

namespace Evidences.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUserService userService;
        private readonly IScoreRepository scoreRepository;

        public ScoreService(
            IUserService userService, 
            IScoreRepository scoreRepository)
        {
            this.userService = userService;
            this.scoreRepository = scoreRepository;
        }

        public async Task Add(Guid songId, int score)
        {
            var user = this.userService.Get();
            if (user == null)
            {
                throw new Exception("Invalid user");
            }
            await scoreRepository.Add(new Score { SongId = songId, JudgeUserId = user.Id, ScoreNumber = score });
        }
    }
}
