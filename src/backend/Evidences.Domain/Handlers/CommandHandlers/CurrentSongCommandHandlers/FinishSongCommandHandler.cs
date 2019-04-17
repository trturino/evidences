using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.CurrentSongCommands;
using Evidences.Domain.Repositories;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.CurrentSongCommandHandlers
{
    public class FinishSongCommandHandler : ICommandHandler<FinishSongCommand, SignalRMessage>
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

        public async Task<SignalRMessage> ExecuteAsync(FinishSongCommand command, SignalRMessage previousResult)
        {
            var currentSong = await _currentSongRepository.FirstOrDefault();
            if (currentSong == null)
            {
                return null;
            }

            var song = await _songRepository.GetById(currentSong.SongId);
            song.Finished = true;

            await _songRepository.Update(song);
            await _currentSongRepository.Remove(currentSong.Id);

            return new SignalRMessage
            {
                Arguments = new object[] { currentSong },
                GroupName = null,
                Target = "finishSongCommandNotification",
            };
        }
    }
}