using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.GetCategory;


public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}