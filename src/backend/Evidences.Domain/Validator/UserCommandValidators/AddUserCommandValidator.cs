using Evidences.Domain.Commands.UserCommands;
using FluentValidation;

namespace Evidences.Domain.Validator.UserCommandValidators
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Invalid user");
        }
    }
}