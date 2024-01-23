using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;

public class UpdateCategoryInputValidator : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id should not be empty");

        // RuleFor(x => x.Name)
        //     .NotEmpty()
        //     .WithMessage("Name should not be empty")
        //     .MinimumLength(3)
        //     .WithMessage("Name should be at least 3 characters long")
        //     .MaximumLength(255)
        //     .WithMessage("Name should be at most 255 characters long");

        // RuleFor(x => x.Description)
        //     .NotEmpty()
        //     .WithMessage("Description should not be empty")
        //     .MaximumLength(10000)
        //     .WithMessage("Description should be at most 10000 characters long");
    }
}