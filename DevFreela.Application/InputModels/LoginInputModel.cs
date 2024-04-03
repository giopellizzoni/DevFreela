namespace DevFreela.Application.InputModels;

public class LoginInputModel
{
    public LoginInputModel(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public string Username { get; private set; }
    public string Password { get; private set; }
}