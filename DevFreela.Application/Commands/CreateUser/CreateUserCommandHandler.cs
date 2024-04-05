using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IAuthservice authservice) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = authservice.ComputeSha256Hash(request.Password);
        var user = new User(request.FullName, request.Email, request.Birthdate, passwordHash, request.Role);
        await userRepository.AddUserAsync(user);
        return user.Id;
    }
}