using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository(DevFreelaDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserAsync(int id)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}