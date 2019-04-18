using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Repositories;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.SongCommandHandlers
{
    public class RemoveSongCommandHandler : ICommandHandler<RemoveSongCommand, SignalRMessage>
    {
        private readonly ISongRepository _songRepository;

        public RemoveSongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<SignalRMessage> ExecuteAsync(RemoveSongCommand command, SignalRMessage previousResult)
        {
            await _songRepository.Remove(command.SongId);

            return new SignalRMessage
            {
                Arguments = new object[] { command },
                GroupName = null,
                Target = "removeSongCommandNotification",
            };
        }
    }
}