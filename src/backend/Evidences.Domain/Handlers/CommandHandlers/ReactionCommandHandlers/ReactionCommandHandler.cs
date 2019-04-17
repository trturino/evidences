using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.ReactionCommands;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.ReactionCommandHandlers
{
    public class ReactionCommandHandler : ICommandHandler<ReactionCommand, SignalRMessage>
    {
        public Task<SignalRMessage> ExecuteAsync(ReactionCommand command, SignalRMessage previousResult)
        {
            return Task.FromResult(new SignalRMessage
            {
                Arguments = new object[] { command },
                GroupName = null,
                Target = "reactionCommandNotification",
            });
        }
    }
}