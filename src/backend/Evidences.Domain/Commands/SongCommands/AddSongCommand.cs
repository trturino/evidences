﻿using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Commands.SongCommands
{
    public class AddSongCommand : ICommand<SignalRMessage>
    {
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
    }
}