using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.CommandHandlers.SongCommandHandlers
{
    public class RemoveSongCommandHandler : ICommandHandler<RemoveSongCommand, bool>
    {
        private readonly ISongRepository _songRepository;

        public RemoveSongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<bool> ExecuteAsync(RemoveSongCommand command, bool previousResult)
        {
            await _songRepository.Remove(command.SongId);

            return true;
        }
    }
}