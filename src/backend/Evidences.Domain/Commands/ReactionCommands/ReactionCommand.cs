using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Evidences.Domain.Commands.ReactionCommands
{
    public class ReactionCommand : ICommand<bool>
    {
        public Guid SondId { get; set; }

        public string Reaction { get; set; }
    }
}