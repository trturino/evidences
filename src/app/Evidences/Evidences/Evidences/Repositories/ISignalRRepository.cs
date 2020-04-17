using System;
using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Models.Requests;
using Refit;

namespace Evidences.Repositories
{
    public interface ISignalRRepository
    {
        [Post("/v1/negotiate")]
        Task<SignalRAuthInfo> GetAuthInfo([Body] AuthInfoRequest user);
    }
}