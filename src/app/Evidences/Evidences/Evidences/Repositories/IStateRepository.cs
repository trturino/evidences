using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface IStateRepository
    {
        [Get("/v1/state")]
        Task<State> Get();
    }
}