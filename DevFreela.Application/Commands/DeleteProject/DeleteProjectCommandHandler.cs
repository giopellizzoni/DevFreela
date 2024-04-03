using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<DeleteProjectCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);
        project?.Cancel();
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}