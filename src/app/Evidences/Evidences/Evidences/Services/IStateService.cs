using System.Threading.Tasks;
using Evidences.Models;

namespace Evidences.Services
{
    public interface IStateService
    {
        Task<State> GetState();
    }
}