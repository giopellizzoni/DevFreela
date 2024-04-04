using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);

        project?.Update(request.Title, request.Description, request.TotalCost);
        await projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}