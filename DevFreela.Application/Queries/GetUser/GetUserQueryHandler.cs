using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserViewModel?>
{
    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserAsync(request.Id);
        return user == null ? 
            null : 
            new UserViewModel(user.FullName, user.Email);
    }
}