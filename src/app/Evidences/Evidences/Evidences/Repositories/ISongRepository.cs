using System;
using System.Threading.Tasks;
using Evidences.Models;
using Refit;

namespace Evidences.Repositories
{
    public interface ISongRepository
    {
        [Post("/v1/song")]
        Task Add(Song song);

        [Delete("/v1/song")]
        Task Remove(Guid songId);
    }
}