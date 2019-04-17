using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.CurrentSongCommands
{
    public class StartSongCommand : ICommand<SignalRMessage>
    {
        public Guid SongId { get; set; }
    }
}