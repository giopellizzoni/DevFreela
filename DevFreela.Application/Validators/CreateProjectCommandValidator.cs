using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{

    public CreateProjectCommandValidator()
    {
        RuleFor(p => p.Description)
            .MaximumLength(255)
            .WithMessage("Max length of description is 255 characters");
        
        RuleFor(p => p.Title)
            .MaximumLength(30)
            .WithMessage("Max length of title is 30 characters");

        RuleFor(p => p.IdClient)
            .NotEqual(0)
            .WithMessage("IdClient cant be 0");
        
        RuleFor(p => p.IdFreelancer)
            .NotEqual(0)
            .WithMessage("IdFreelancer cant be 0");
    }
}