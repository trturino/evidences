using System.Threading.Tasks;
using Evidences.Domain.Models;
namespace Evidences.Domain.Repositories
{
    public interface ICurrentSongRepository : IRepository<CurrentSong>
    {
        Task<CurrentSong> FirstOrDefault();
    }
}
