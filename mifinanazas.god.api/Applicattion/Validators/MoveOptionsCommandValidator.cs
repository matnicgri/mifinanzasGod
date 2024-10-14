using FluentValidation;
using Mifinanazas.God.Applicattion.Features.Game.Commands;

namespace Mifinanazas.God.Applicattion.Validators
{
    public class MoveOptionsCommandValidator : AbstractValidator<MoveOptionsCommand>
    {
        public MoveOptionsCommandValidator()
        {            
            RuleFor(command => command.moveOptions)
                .NotNull().WithMessage("Move options must be provided.")
                .NotEmpty().WithMessage("Move options cannot be empty.");

            RuleForEach(command => command.moveOptions).ChildRules(moveOption =>
            {
                moveOption.RuleFor(m => m.id)
                    .GreaterThan(-2).WithMessage("Move option ID must be posiitve.");

                moveOption.RuleFor(m => m.description)
                    .NotEmpty().WithMessage("Move option description cannot be empty.")
                    .MaximumLength(100).WithMessage("Move option description cannot exceed 100 characters.");

                moveOption.RuleFor(m => m.killId)
                    .GreaterThanOrEqualTo(0).WithMessage("Kill ID must be a non-negative number.");
            });
        }
    }
}
