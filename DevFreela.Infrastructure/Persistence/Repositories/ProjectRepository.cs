using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private const int PAGE_SIZE = 10; 
    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Project>> GetAllAsync(string query, int page)
    {
        IQueryable<Project>? projects = _dbContext.Projects;
        if (!string.IsNullOrEmpty(query))
        {
            projects = projects?.Where(p => p.Title.Contains(query) || p.Description.Contains(query));
        }

        return await projects!.GetPaged<Project>(page, PAGE_SIZE);
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbContext.Projects!
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects!.AddAsync(project);
    }

    public async Task AddCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.Comments!.AddAsync(projectComment );
    }

}