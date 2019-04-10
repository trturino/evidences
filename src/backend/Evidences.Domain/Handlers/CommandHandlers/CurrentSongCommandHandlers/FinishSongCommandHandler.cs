using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.CurrentSongCommands;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.CommandHandlers.CurrentSongCommandHandlers
{
    public class FinishSongCommandHandler : ICommandHandler<FinishSongCommand, bool>
    {
        private readonly ICurrentSongRepository _currentSongRepository;
        private readonly ISongRepository _songRepository;

        public FinishSongCommandHandler(
            ICurrentSongRepository currentSongRepository,
            ISongRepository songRepository
            )
        {
            _currentSongRepository = currentSongRepository;
            _songRepository = songRepository;
        }

        public async Task<bool> ExecuteAsync(FinishSongCommand command, bool previousResult)
        {
            var currentSong = await _currentSongRepository.FirstOrDefault();
            if (currentSong == null)
            {
                return false;
            }

            var song = await _songRepository.GetById(currentSong.SongId);
            song.Finished = true;

            await _songRepository.Update(song);
            await _currentSongRepository.Remove(currentSong.Id);

            return true;
        }
    }
}