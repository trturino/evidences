using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.SongCommandValidator
{
    public class AddSongCommandValidator : AbstractValidator<AddSongCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddSongCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Thumbnail).NotEmpty().WithMessage("Invalid thumbnail");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Invalid title");
            RuleFor(x => x.AddedByUser).MustAsync(ValidateUserId).WithMessage("Invalid user");
        }

        private async Task<bool> ValidateUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.Exists(userId);
        }
    }
}