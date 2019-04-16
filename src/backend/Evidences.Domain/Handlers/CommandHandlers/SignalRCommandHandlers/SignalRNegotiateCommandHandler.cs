using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SignalRCommands;
using FunctionMonkey.Commanding.Abstractions;

namespace Evidences.Domain.Handlers.CommandHandlers.SignalRCommandHandlers
{
    public class SignalRNegotiateCommandHandler : ICommandHandler<SignalRNegotiateCommand, SignalRNegotiateResponse>
    {
        public Task<SignalRNegotiateResponse> ExecuteAsync(SignalRNegotiateCommand command, SignalRNegotiateResponse previousResult)
        {
            return Task.FromResult(new SignalRNegotiateResponse
            {
                HubName = "karaokeHub",
                UserId = command.UserId.ToString()
            });
        }
    }
}
