using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.ScoreCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.ScoreCommandValidators
{
    public class SetScoreCommandValidator : AbstractValidator<SetScoreCommand>
    {
        private readonly ISongRepository _songRepository;
        private readonly IUserRepository _userRepository;

        public SetScoreCommandValidator(
                ISongRepository songRepository,
                IUserRepository userRepository
            )
        {
            _songRepository = songRepository;
            _userRepository = userRepository;

            RuleFor(x => x.SongId).MustAsync(ValidateSongId).WithMessage("Invalid Song");
            RuleFor(x => x.JudgeUserId).MustAsync(ValidateUserId).WithMessage("Ivalid judge user");
        }

        private async Task<bool> ValidateSongId(Guid songId, CancellationToken cancellationToken)
        {
            return await _songRepository.Exists(songId);
        }

        private async Task<bool> ValidateUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.Exists(userId);
        }
    }
}