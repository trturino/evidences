using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;
using Evidences.Domain.Queries.StateQuery;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.QueryHandlers.StateQueryHandlers
{
    public class GetStateQueryHandler : ICommandHandler<GetStateQuery, State>
    {
        private readonly ICurrentSongRepository _currentSongRepository;
        private readonly ISongRepository _songRepository;

        public GetStateQueryHandler(ICurrentSongRepository currentSongRepository, ISongRepository songRepository)
        {
            _currentSongRepository = currentSongRepository;
            _songRepository = songRepository;
        }

        public async Task<State> ExecuteAsync(GetStateQuery command, State previousResult)
        {
            var currentSong = await _currentSongRepository.FirstOrDefault();
            var queue = await _songRepository.GetAll(x => x.Finished == false);

            if (currentSong != null) 
            {
                queue = queue.Where(x => x.Id != currentSong.SongId);
            }

            var state = new State()
            {
                CurrentSong = currentSong,
                Queue = queue.ToList()
            };

            return state;
        }
    }
}