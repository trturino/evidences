using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.ReactionCommands
{
    public class ReactionCommand : ICommand<SignalRMessage>
    {
        public Guid SondId { get; set; }

        public string Reaction { get; set; }
    }
}