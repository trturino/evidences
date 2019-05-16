using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SessionCommands;
using Evidences.Domain.Repositories;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.SessionCommandHandlers
{
    public class StartSessionCommandHandler : ICommandHandler<StartSessionCommand, SignalRMessage>
    {
        private readonly ISessionRepository _sessionRepository;

        public StartSessionCommandHandler(
            ISessionRepository sessionRepository
            )
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<SignalRMessage> ExecuteAsync(StartSessionCommand command, SignalRMessage previousResult)
        {

            var session = new Models.Session()
            {
                Id = Guid.NewGuid()
            };

            await _sessionRepository.Add(session);

            return new SignalRMessage
            {
                Arguments = new object[] { session },
                GroupName = null,
                Target = "startSessionCommandNotification",
            };
        }
    }
}