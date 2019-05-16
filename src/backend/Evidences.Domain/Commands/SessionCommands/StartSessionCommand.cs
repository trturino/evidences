using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.SessionCommands
{
    public class StartSessionCommand : ICommand<SignalRMessage>
    {
    }
}