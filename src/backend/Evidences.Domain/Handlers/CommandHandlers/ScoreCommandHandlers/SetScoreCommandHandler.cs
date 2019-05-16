using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.ScoreCommands;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;
using System.Linq;

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

            var actualScore = (await _scoreRepository.GetAll($"select * from c where c.judgeUserId = '{command.JudgeUserId}' and c.songId = '{command.SongId}'")).FirstOrDefault();

            if (actualScore != null)
            {
                actualScore.ScoreNumber = command.ScoreNumber;
                actualScore.ScoredAt = DateTime.UtcNow;

                await _scoreRepository.Update(actualScore);
                return actualScore;
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