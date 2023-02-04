using FluentValidation;
namespace IteratesBrewManager.Application.Breweries.Commands.UpdateBrewery;

public class UpdateBreweryCommandValidator : AbstractValidator<UpdateBreweryCommand>
{
    public UpdateBreweryCommandValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0);
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        //TODO: Add rules for brewer name not duplicated
    }
}
