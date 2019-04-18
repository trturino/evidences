using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.SongCommands
{
    public class RemoveSongCommand : ICommand<SignalRMessage>
    {
        public Guid SongId { get; set; }
    }
}