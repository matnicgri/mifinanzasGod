using FluentValidation;
using Mifinanazas.God.Applicattion.Features.Game.Commands;

namespace Mifinanazas.God.Applicattion.Validators
{
    public class GameCommandValidator : AbstractValidator<GameCommand>
    {
        public GameCommandValidator()
        {
            RuleFor(x => x.player1Name)
                .NotEmpty().WithMessage("Player 1 name is required.")
                .Length(1, 100).WithMessage("Player 1 name must be between 1 and 100 characters.");

            RuleFor(x => x.player2Name)
                    .NotEmpty().WithMessage("Player 2 name is required.")
                    .Length(1, 100).WithMessage("Player 2 name must be between 1 and 100 characters.");
        }
    }
}
