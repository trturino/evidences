using System.Threading.Tasks;
using Evidences.Models;

namespace Evidences.Services
{
    public interface IUserService
    {
        Task Add(string userName);

        User Get();
    }
}