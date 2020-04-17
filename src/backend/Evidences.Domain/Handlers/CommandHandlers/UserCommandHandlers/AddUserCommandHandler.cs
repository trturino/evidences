using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Commands.UserCommands;
using Evidences.Domain.Models;
using Evidences.Domain.Repositories;

namespace Evidences.Domain.Handlers.CommandHandlers.UserCommandHandlers
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User> ExecuteAsync(AddUserCommand command, User previousResult)
        {
            var user = new User()
            {
                Added = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                UserName = command.UserName
            };

            await _userRepository.Add(user);

            return user;
        }
    }
}