using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;
using FunctionMonkey.Abstractions.SignalR;

namespace Evidences.Domain.Handlers.CommandHandlers.SongCommandHandlers
{
    public class AddSongCommandHandler : ICommandHandler<AddSongCommand, SignalRMessage>
    {
        private readonly ISongRepository _songRepository;

        public AddSongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<SignalRMessage> ExecuteAsync(AddSongCommand command, SignalRMessage previousResult)
        {
            var song = new Song()
            {
                AddedByUser = command.AddedByUser,
                AddedAt = DateTimeOffset.UtcNow,
                Author = command.Author,
                Description = command.Description,
                Duration = command.Duration,
                Finished = false,
                Id = Guid.NewGuid(),
                NoAuthor = command.NoAuthor,
                NoDescription = command.NoDescription,
                Thumbnail = command.Thumbnail,
                Title = command.Title,
                Url = command.Url,
                ViewCount = command.ViewCount
            };

            await _songRepository.Add(song);

            return new SignalRMessage
            {
                Arguments = new object[] { song },
                GroupName = null,
                Target = "addSongCommandNotification",
            };
        }
    }
}