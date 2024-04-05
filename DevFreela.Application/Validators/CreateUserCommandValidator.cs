using System.Text.RegularExpressions;
using DevFreela.Application.Commands.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("Invalid Email");

        RuleFor(p => p.Password)
            .Must(ValidatePassword!)
            .WithMessage("Password must contain at least 8 characters, a number, a capital letter, a non capital letter and one special char");

        RuleFor(p => p.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name is mandatory");
    }

    private bool ValidatePassword(string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=].*$)");
        return regex.IsMatch(password);
    }
}