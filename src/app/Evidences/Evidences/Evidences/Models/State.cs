using System;
using System.Collections.Generic;
using System.Text;

namespace Evidences.Models
{
    public class State
    {
        public CurrentSong CurrentSong { get; set; }

        public ICollection<Song> Queue { get; set; }
    }
}
