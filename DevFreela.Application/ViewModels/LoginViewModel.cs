namespace DevFreela.Application.ViewModels;

public class LoginViewModel(string? email, string token)
{
    public string? Email { get; private set; } = email;
    public string Token { get; private set; } = token;
}