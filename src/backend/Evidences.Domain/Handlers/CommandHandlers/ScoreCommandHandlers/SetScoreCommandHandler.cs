using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.ScoreCommands;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.CommandHandlers.ScoreCommandHandlers
{
    public class SetScoreCommandHandler : ICommandHandler<SetScoreCommand, Score>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ICurrentSongRepository _currentSongRepository;

        public SetScoreCommandHandler(
                IScoreRepository scoreRepository,
                ICurrentSongRepository currentSongRepository
            )
        {
            _scoreRepository = scoreRepository;
            _currentSongRepository = currentSongRepository;
        }

        public async Task<Score> ExecuteAsync(SetScoreCommand command, Score previousResult)
        {
            var currentSong = await _currentSongRepository.FirstOrDefault();
            if (currentSong == null)
            {
                return null;
            }

            var score = new Score()
            {
                Id = Guid.NewGuid(),
                JudgeUserId = command.JudgeUserId,
                ScoredAt = DateTimeOffset.UtcNow,
                ScoreNumber = command.ScoreNumber,
                SingerUserId = currentSong.AddedByUser,
                SongId = currentSong.SongId
            };

            await _scoreRepository.Add(score);

            return score;
        }
    }
}