using System;

namespace Evidences.Domain.Models
{
    public class CurrentSong : Song
    {
        public Guid SongId { get; set; }

        public DateTimeOffset StartedAt { get; set; }

        public string UserName { get; set; }
    }
}