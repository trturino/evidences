using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;

namespace Evidences.Domain.Commands.ScoreCommands
{
    public class SetScoreCommand : ICommand<Score>
    {
        public Guid SongId { get; set; }

        public Guid JudgeUserId { get; set; }

        public int ScoreNumber { get; set; }
    }
}