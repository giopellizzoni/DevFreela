using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<FinishProjectCommand, Unit>
{
    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project =
            await dbContext.Projects
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        project?.Finish();
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}