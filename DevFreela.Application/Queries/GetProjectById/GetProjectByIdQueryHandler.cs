using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel?>
{
    public async Task<ProjectDetailsViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        
        if (project == null) return null;
        var projectsDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client?.FullName,
            project.Freelancer?.FullName
        );
        return projectsDetailsViewModel;
    }
}