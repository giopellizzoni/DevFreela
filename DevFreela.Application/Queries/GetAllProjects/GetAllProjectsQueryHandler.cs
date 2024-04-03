using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
    : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
{
    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        
        var projects = dbContext.Projects;
        var projectsViewModel = await projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt))
            .ToListAsync(cancellationToken);
        return projectsViewModel;
    }
}