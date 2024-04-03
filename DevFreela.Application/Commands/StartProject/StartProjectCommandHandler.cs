using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<StartProjectCommand, Unit>
{
    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    { 
        var project = await dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        project?.Start();
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}