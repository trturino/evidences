using System.Threading.Tasks;
using Evidences.Models;

namespace Evidences.Services
{
    public interface ISongService
    {
        Task Add(Song song);
    }
}