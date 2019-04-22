using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.SignalRCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.SignalRCommandValidators
{
    public class SignalRNegotiateCommandValidator : AbstractValidator<SignalRNegotiateCommand>
    {
        private readonly IUserRepository _userRepository;

        private static Guid WEB_PLAYER_ID = new Guid("8AC6F07F-62AE-4D86-AEF7-0299FF245BE2");

        public SignalRNegotiateCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserId).MustAsync(ValidateUserId).WithMessage("Ivalid user id");
        }

        private async Task<bool> ValidateUserId(Guid userId, CancellationToken cancellationToken)
        {
            if (userId == WEB_PLAYER_ID)
            {
                return true;
            }

            return await _userRepository.Exists(userId);
        }
    }
}