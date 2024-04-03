using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommand(int id) : IRequest<Unit>
{
    public int Id { get; private set; } = id;
}