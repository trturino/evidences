using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.CurrentSongCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.CurrentSongCommandValidators
{
    public class StartSongCommandValidator : AbstractValidator<StartSongCommand>
    {
        private readonly ISongRepository _songRepository;

        public StartSongCommandValidator(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public StartSongCommandValidator()
        {
            RuleFor(x => x.SongId).MustAsync(ValidateSongId).WithMessage("Invalid song");
        }

        private async Task<bool> ValidateSongId(Guid songId, CancellationToken cancellationToken)
        {
            return await _songRepository.Exists(songId);
        }
    }
}