using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
{
    private readonly IUnityOfWork _unityOfWork;

    public UpdateProjectCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unityOfWork.Projects.GetByIdAsync(request.Id);
        await _unityOfWork.BeginTransactionAsync();
        project?.Update(request.Title, request.Description, request.TotalCost);
        await _unityOfWork.SaveChangesAsync();
        await _unityOfWork.CommitTransactionAsync();
        return Unit.Value;
    }
}