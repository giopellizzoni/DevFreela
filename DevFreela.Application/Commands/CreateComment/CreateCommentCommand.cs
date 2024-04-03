using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Unit>
{
    
    public required string Comment { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}