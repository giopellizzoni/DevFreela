using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IUnityOfWork _unityOfWork;

    public DeleteProjectCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unityOfWork.Projects.GetByIdAsync(request.Id);
        await _unityOfWork.BeginTransactionAsync();
        project?.Cancel();
        await _unityOfWork.SaveChangesAsync();
        await _unityOfWork.CommitTransactionAsync();
        return Unit.Value;
    }
}