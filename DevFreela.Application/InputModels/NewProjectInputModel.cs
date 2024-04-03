namespace DevFreela.Application.InputModels;

public class NewProjectInputModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
    
}