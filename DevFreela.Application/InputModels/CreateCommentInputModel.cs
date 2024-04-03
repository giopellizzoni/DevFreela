namespace DevFreela.Application.InputModels;

public class CreateCommentInputModel
{
    public string Comment { get; set; }
    public int IdProject { get; set; }
    public int IdUser { get; set; }
}