using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel?>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetUserQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unityOfWork.Users.GetUserAsync(request.Id);
        return user == null ? 
            null : 
            new UserViewModel(user.FullName, user.Email, user.Birthdate);
    }
}