using FluentValidation;

namespace IteratesBrewManager.Application.Beers.Commands.CreateBeer;

public class CreateBeerCommandValidator : AbstractValidator<CreateBeerCommand>
{
    public CreateBeerCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.BrewerId)
            .GreaterThan(0);
        //TODO: Add rules for brewer must exists
    }
}
