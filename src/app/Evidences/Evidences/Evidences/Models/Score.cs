using System;

namespace Evidences.Models
{
    public class Score
    {
        public Guid SongId { get; set; }

        public Guid JudgeUserId { get; set; }

        public int ScoreNumber { get; set; }
    }
}