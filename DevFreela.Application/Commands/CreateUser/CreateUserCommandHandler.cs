using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IAuthService _authService;

    public CreateUserCommandHandler(IUnityOfWork unityOfWork, IAuthService authService)
    {
        _unityOfWork = unityOfWork;
        _authService = authService;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);
        var user = new User(request.FullName, request.Email, request.Birthdate, passwordHash, request.Role);
        
        await _unityOfWork.BeginTransactionAsync();
        await _unityOfWork.Users.AddUserAsync(user);
        await _unityOfWork.SaveChangesAsync();
        await _unityOfWork.CommitTransactionAsync();
        
        return user.Id;
    }
}