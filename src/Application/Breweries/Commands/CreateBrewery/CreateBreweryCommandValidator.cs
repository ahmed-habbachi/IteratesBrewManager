using FluentValidation;
namespace IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;

public class CreateBreweryCommandValidator : AbstractValidator<CreateBreweryCommand>
{
    public CreateBreweryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        //TODO: Add rules for brewer name not duplicated
    }
}
