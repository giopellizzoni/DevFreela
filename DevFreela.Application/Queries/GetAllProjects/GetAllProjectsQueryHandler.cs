using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler(IProjectRepository _projectRepository)
    : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
{
    public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();
    }
}