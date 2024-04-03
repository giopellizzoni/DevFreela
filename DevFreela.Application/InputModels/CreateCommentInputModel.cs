namespace DevFreela.Application.InputModels;

public class CreateCommentInputModel
{
    public required string Comment { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}