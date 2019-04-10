using System;
using System.Threading;
using System.Threading.Tasks;
using Evidences.Domain.Commands.ReactionCommands;
using Evidences.Domain.Repositories;
using FluentValidation;

namespace Evidences.Domain.Validator.ReactionCommandValidators
{
    public class ReactionCommandValidator : AbstractValidator<ReactionCommand>
    {
        private readonly ISongRepository _songRepository;

        public ReactionCommandValidator(ISongRepository songRepository)
        {
            _songRepository = songRepository;

            RuleFor(x => x.SondId).MustAsync(ValidateSongId).WithMessage("Invalid song");
            RuleFor(x => x.Reaction).NotEmpty().WithMessage("Invalid reaction");
        }

        private async Task<bool> ValidateSongId(Guid songId, CancellationToken cancellationToken)
        {
            return await _songRepository.Exists(songId);
        }
    }
}