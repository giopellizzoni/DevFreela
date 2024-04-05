namespace DevFreela.Application.ViewModels;

public class UserViewModel(string? username, string? email, DateTime birthDate)
{
    public string? Username { get; private set; } = username;
    public string? Email { get; private set; } = email;
    public DateTime BirthDate { get; set; } = birthDate;
}
