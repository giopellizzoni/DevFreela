using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetUser;

public class GetUserQueryHandler(DevFreelaDbContext dbContext) : IRequestHandler<GetUserQuery, UserViewModel?>
{
    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user == null) return null;
        return new UserViewModel(user.FullName, user.Email);
    }
}