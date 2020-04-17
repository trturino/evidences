using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface IReactionRepository
    {
        [Post("/v1/reaction")]
        Task Send([Body] Reactions reactions);
    }
}