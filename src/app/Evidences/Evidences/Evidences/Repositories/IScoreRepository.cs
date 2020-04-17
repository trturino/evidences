using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface IScoreRepository
    {
        [Post("/v1/song/score")]
        Task Add([Body] Score score);
    }
}