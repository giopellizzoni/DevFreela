using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<UpdateProjectCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

        project?.Update(request.Title, request.Description, request.TotalCost);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}