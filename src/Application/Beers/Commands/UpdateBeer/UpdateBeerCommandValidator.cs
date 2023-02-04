using FluentValidation;

namespace IteratesBrewManager.Application.Beers.Commands.UpdateBeer;

public class UpdateBeerCommandValidator : AbstractValidator<UpdateBeerCommand>
{
    public UpdateBeerCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.BrewerId)
            .GreaterThan(0);
    }
}
