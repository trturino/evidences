using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Repositories;

namespace Evidences.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task Add(Song song)
        {
            await _songRepository.Add(song);
        }
    }
}