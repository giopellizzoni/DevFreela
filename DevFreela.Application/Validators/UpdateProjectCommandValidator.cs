using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class UpdateProjectCommandValidator: AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description can't be empty.");
        
        RuleFor(p => p.Description)
            .MaximumLength(255)
            .WithMessage("Max length of description is 255 characters");
    }
}