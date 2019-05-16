using System.Collections.Generic;

namespace Evidences.Domain.Models
{
    public class State
    {
        public CurrentSong CurrentSong { get; set; }

        public bool HasSession { get; set; }

        public ICollection<Song> Queue { get; set; }
    }
}