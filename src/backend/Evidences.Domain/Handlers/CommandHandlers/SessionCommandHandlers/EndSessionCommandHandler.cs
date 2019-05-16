using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SessionCommands;
using Evidences.Domain.Repositories;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.SessionCommandHandlers
{
    public class EndSessionCommandHandler : ICommandHandler<EndSessionCommand, SignalRMessage>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISongRepository _songRepository;
        private readonly ICurrentSongRepository _currentSongRepository;
        private readonly IScoreRepository _scoreRepository;

        public EndSessionCommandHandler(
            ISessionRepository sessionRepository,
            ISongRepository songRepository,
            ICurrentSongRepository currentSongRepository,
            IScoreRepository scoreRepository
            )
        {
            _sessionRepository = sessionRepository;
            _songRepository = songRepository;
            _currentSongRepository = currentSongRepository;
            _scoreRepository = scoreRepository;
        }

        public async Task<SignalRMessage> ExecuteAsync(EndSessionCommand command, SignalRMessage previousResult)
        {
            await _sessionRepository.Clear();
            await _songRepository.Clear();
            await _currentSongRepository.Clear();
            await _scoreRepository.Clear();

            return new SignalRMessage
            {
                Arguments = new object[] {  },
                GroupName = null,
                Target = "endSessionCommandNotification",
            };
        }
    }
}