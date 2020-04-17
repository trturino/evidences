using System;
using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Repositories;

namespace Evidences.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<State> GetState()
        {
            return await _stateRepository.Get();
        }
    }
}