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

        public SignalRNegotiateCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserId).MustAsync(ValidateUserId).WithMessage("Ivalid user id");
        }

        private async Task<bool> ValidateUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.Exists(userId);
        }
    }
}
