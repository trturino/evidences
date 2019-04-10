using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Evidences.Domain.Commands.SongCommands
{
    public class RemoveSongCommand : ICommand<bool>
    {
        public Guid SongId { get; set; }
    }
}