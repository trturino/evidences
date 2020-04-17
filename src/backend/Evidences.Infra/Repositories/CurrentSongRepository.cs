using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Infra.Repositories
{
    public class CurrentSongRepository : Repository<CurrentSong>, ICurrentSongRepository
    {
        public CurrentSongRepository(ICosmosStore<CurrentSong> cosmosStore) : base(cosmosStore)
        {
        }


    }
}