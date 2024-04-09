using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginViewModel?>
{
    private readonly IAuthService _authService;
    private readonly IUnityOfWork _unityOfWork;

    public LoginUserCommandHandler(IAuthService authService, IUnityOfWork unityOfWork)
    {
        _authService = authService;
        _unityOfWork = unityOfWork;
    }

    public async Task<LoginViewModel?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);
        var user = await _unityOfWork.Users.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
        if (user == null) return null;

        var token = _authService.GenerateJwtToken(user.Email, user.Role);
        return new LoginViewModel(user.Email, token);
    }
}