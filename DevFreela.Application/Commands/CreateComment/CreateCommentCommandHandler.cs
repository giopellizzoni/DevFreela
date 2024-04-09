using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
{
    private readonly IUnityOfWork _unityOfWork;

    public CreateCommentCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new ProjectComment(request.Comment, request.IdProject, request.IdUser);
        await _unityOfWork.BeginTransactionAsync();

        await _unityOfWork.Projects.AddCommentAsync(comment);
        await _unityOfWork.SaveChangesAsync();

        await _unityOfWork.CommitTransactionAsync();
        return Unit.Value;
    }
}