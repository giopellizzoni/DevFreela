using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
{
    private readonly IUnityOfWork _unityOfWork;

    public StartProjectCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unityOfWork.Projects.GetByIdAsync(request.Id);
        await _unityOfWork.BeginTransactionAsync();
        project?.Start();
        await _unityOfWork.SaveChangesAsync();
        await _unityOfWork.CommitTransactionAsync();
        return Unit.Value;
    }
}