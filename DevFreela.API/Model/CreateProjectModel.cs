namespace DevFreela.API.Model;

public class CreateProjectModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}
