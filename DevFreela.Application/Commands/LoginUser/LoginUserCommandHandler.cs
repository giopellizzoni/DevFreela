using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;

public class LoginUserCommandHandler(IAuthservice authService, IUserRepository userRepository)
    : IRequestHandler<LoginUserCommand, LoginViewModel?>
{

    public async Task<LoginViewModel?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = authService.ComputeSha256Hash(request.Password);
        var user = await userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
        if (user == null) return null;

        var token = authService.GenerateJwtToken(user.Email, user.Role);
        return new LoginViewModel(user.Email, token);
    }
}