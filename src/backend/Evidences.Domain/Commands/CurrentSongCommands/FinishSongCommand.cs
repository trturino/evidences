using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.CurrentSongCommands
{
    public class FinishSongCommand : ICommand<SignalRMessage>
    {
    }
}