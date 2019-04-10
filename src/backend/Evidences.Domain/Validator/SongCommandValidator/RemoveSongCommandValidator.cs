using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.SongCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.SongCommandValidator
{
    public class RemoveSongCommandValidator : AbstractValidator<RemoveSongCommand>
    {
        private readonly ISongRepository _songRepository;

        public RemoveSongCommandValidator(
                ISongRepository songRepository
            )
        {
            _songRepository = songRepository;

            RuleFor(x => x.SongId).MustAsync(ValidateSongId).WithMessage("Invalid song");
        }

        private async Task<bool> ValidateSongId(Guid songId, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetById(songId);
            return song != null && !song.Finished;
        }
    }
}