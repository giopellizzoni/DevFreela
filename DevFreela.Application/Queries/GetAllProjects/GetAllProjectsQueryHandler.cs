using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
{
    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync();
        return projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();
    }
}