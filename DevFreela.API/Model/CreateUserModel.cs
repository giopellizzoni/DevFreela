namespace DevFreela.API.Model;

public class CreateUserModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
