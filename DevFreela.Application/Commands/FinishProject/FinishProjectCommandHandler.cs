using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<FinishProjectCommand, Unit>
{
    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        project?.Finish();
        await projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}