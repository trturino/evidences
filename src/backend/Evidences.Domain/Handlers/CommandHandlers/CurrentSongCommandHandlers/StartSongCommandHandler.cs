using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.CurrentSongCommands;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.CommandHandlers.CurrentSongCommandHandlers
{
    public class StartSongCommandHandler : ICommandHandler<StartSongCommand, CurrentSong>
    {
        private readonly ISongRepository _songRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentSongRepository _currentSongRepository;

        public StartSongCommandHandler(
            ISongRepository songRepository,
            IUserRepository userRepository,
            ICurrentSongRepository currentSongRepository
            )
        {
            _songRepository = songRepository;
            _userRepository = userRepository;
            _currentSongRepository = currentSongRepository;
        }

        public async Task<CurrentSong> ExecuteAsync(StartSongCommand command, CurrentSong previousResult)
        {
            var song = await _songRepository.GetById(command.SongId);
            if (song == null)
            {
                return null;
            }

            var user = await _userRepository.GetById(song.AddedByUser);
            var currentSong = await _currentSongRepository.FirstOrDefault();
            if (currentSong != null)
            {
                await _currentSongRepository.Remove(currentSong.Id);
            }

            var actualCurrentSong = new CurrentSong
            {
                AddedAt = song.AddedAt,
                AddedByUser = song.AddedByUser,
                Author = song.Author,
                Description = song.Description,
                Duration = song.Duration,
                Finished = false,
                Id = Guid.NewGuid(),
                NoAuthor = song.NoAuthor,
                NoDescription = song.NoDescription,
                SongId = song.Id,
                StartedAt = DateTimeOffset.UtcNow,
                Thumbnail = song.Thumbnail,
                Title = song.Title,
                Url = song.Url,
                ViewCount = song.ViewCount,
                UserName = user.UserName
            };

            await _currentSongRepository.Add(actualCurrentSong);

            return actualCurrentSong;
        }
    }
}