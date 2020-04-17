using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;
using Evidences.Domain.Queries.SongsQueries;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.QueryHandlers.SongsQueryHandler
{
    public class GetSongsQueryHandler : ICommandHandler<GetSongsQuery, IEnumerable<Song>>
    {
        private readonly ISongRepository _songRepository;

        public GetSongsQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<IEnumerable<Song>> ExecuteAsync(GetSongsQuery command, IEnumerable<Song> previousResult)
        {
            return await _songRepository.GetAll();
        }
    }
}