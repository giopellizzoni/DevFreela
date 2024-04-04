using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<StartProjectCommand, Unit>
{
    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        project?.Start();
        await projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}