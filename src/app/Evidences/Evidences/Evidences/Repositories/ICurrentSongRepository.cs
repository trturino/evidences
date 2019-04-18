using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface ICurrentSongRepository
    {
        [Get("/v1/song/current")]
        Task<CurrentSong> Get();
    }
}