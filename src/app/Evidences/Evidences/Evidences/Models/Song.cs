using System;
using Evidences.YouTube;

namespace Evidences.Models
{
    public class Song
    {
        public Song()
        {

        }

        public Song(VideoInformation video, Guid userId)
        {
            Title = video.Title;
            Author = video.Author;
            Description = video.Description;
            Duration = video.Duration;
            Url = video.Url;
            Thumbnail = video.Thumbnail;
            NoAuthor = video.NoAuthor;
            NoDescription = video.NoDescription;
            ViewCount = video.ViewCount;
            AddedByUser = userId;
        }

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

        public string AddedByUserName { get; set; }
    }
}