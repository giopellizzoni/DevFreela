using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IUnityOfWork _unityOfWork;

    public CreateProjectCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Title, request.Description, request.IdClient,
            request.IdFreelancer, request.TotalCost);
        await _unityOfWork.BeginTransactionAsync();
        
        await _unityOfWork.Projects.AddAsync(project);
        await _unityOfWork.SaveChangesAsync();
        
        await _unityOfWork.CommitTransactionAsync();
        return project.Id;
    }
}