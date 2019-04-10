using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;
using Evidences.Domain.Queries.CurrentSongQueries;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.QueryHandlers.CurrentSongQueryHandlers
{
    public class GetCurrentSongQueryHandler : ICommandHandler<GetCurrentSongQuery, CurrentSong>
    {
        private readonly ICurrentSongRepository _currentSongRepository;

        public GetCurrentSongQueryHandler(ICurrentSongRepository currentSongRepository)
        {
            _currentSongRepository = currentSongRepository;
        }

        public async Task<CurrentSong> ExecuteAsync(GetCurrentSongQuery command, CurrentSong previousResult)
        {
            var currentSong = await _currentSongRepository.FirstOrDefault();

            return currentSong;
        }
    }
}