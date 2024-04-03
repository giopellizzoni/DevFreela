using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<CreateCommentCommand, Unit>
{
    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new ProjectComment(request.Comment, request.IdProject, request.IdUser);
        await dbContext.Comments.AddAsync(comment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}