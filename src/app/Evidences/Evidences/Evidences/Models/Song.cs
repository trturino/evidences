using System;

namespace Evidences.Models
{
    public class Song
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public string Url { get; set; }

        public string Thumbnail { get; set; }

        public bool NoDescription { get; set; }

        public bool NoAuthor { get; set; }

        public string ViewCount { get; set; }

        public Guid AddedByUser { get; set; }

        public DateTimeOffset AddedAt { get; set; }
    }
}