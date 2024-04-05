using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(p => p.Comment)
            .NotEmpty()
            .WithMessage("Comment can't be empty.");
        
        RuleFor(p => p.Comment)
            .MaximumLength(512)
            .WithMessage("Comment must have up to 512 characters");
            
    }
}