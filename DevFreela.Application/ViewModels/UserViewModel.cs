namespace DevFreela.Application.ViewModels;

public class UserViewModel
{
    public UserViewModel(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
}