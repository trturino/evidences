using System.Threading.Tasks;
using Evidences.Models;
using Refit;
using Evidences.Models.Requests;

namespace Evidences.Repositories
{
    public interface IUserRepository
    {
        [Post("/v1/user")]
        Task<User> Add([Body] AddUserRequest user);
    }
}