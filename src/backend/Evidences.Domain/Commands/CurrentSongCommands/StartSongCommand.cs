using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;

namespace Evidences.Domain.Commands.CurrentSongCommands
{
    public class StartSongCommand : ICommand<CurrentSong>
    {
        public Guid SongId { get; set; }
    }
}