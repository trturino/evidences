using System;

namespace Evidences.Domain.Models
{
    public class Score : Model
    {
        public Guid SongId { get; set; }

        public Guid SingerUserId { get; set; }

        public Guid JudgeUserId { get; set; }

        public int ScoreNumber { get; set; }

        public DateTimeOffset ScoredAt { get; set; }
    }
}