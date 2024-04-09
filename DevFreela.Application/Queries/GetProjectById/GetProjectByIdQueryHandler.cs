using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel?>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetProjectByIdQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<ProjectDetailsViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _unityOfWork.Projects.GetByIdAsync(request.Id);
        
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