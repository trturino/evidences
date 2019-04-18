using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface IUserRepository
    {
        [Post("/v1/user")]
        Task<User> Add([Body] string userName);
    }
}