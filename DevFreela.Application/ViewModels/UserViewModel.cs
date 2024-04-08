namespace DevFreela.Application.ViewModels;

public class UserViewModel
{
    public UserViewModel(string? username, string? email, DateTime birthDate)
    {
        Username = username;
        Email = email;
        BirthDate = birthDate;
    }

    public string? Username { get; private set; }
    public string? Email { get; private set; }
    public DateTime BirthDate { get; set; }
}
