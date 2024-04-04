using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateCommentCommand, Unit>
{
    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new ProjectComment(request.Comment, request.IdProject, request.IdUser);
        await projectRepository.AddCommentAsync(comment);
        return Unit.Value;
    }
}