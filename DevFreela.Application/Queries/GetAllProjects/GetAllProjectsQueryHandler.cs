using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetAllProjectsQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var paginationProjects = await _unityOfWork.Projects.GetAllAsync(request.Query, request.Page);
        var projectsViewModel = paginationProjects
            .Data
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();
        var paginationProjectsViewModel = new PaginationResult<ProjectViewModel>(
            paginationProjects.Page,
            paginationProjects.TotalPages, 
            paginationProjects.PageSize, 
            paginationProjects.ItemsCount,
            projectsViewModel);
        
        return paginationProjectsViewModel;
    }
}