using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string? FullName { get; private set; }
    public string? Email { get; private set; }
    public DateTime Birthdate { get; private set; }
}