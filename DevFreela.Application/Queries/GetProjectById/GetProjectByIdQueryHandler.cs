using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
    : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel?>
{
    public async Task<ProjectDetailsViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (project == null) return null;
        var projectsDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client.FullName,
            project.Freelancer.FullName
        );
        return projectsDetailsViewModel;
    }
}