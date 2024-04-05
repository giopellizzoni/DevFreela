using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository(DevFreelaDbContext dbContext) : IProjectRepository
{
    public async Task<List<Project>> GetAllAsync()
    {
        return await dbContext.Projects!.ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await dbContext.Projects!
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await dbContext.Projects!.AddAsync(project);
        await SaveChangesAsync();
    }

    public async Task AddCommentAsync(ProjectComment projectComment)
    {
        await dbContext.Comments!.AddAsync(projectComment );
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}