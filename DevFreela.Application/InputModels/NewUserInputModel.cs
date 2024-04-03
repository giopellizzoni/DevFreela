namespace DevFreela.Application.InputModels;

public class NewUserInputModel
{
    public NewUserInputModel(string fullName, string email, DateTime birthdate)
    {
        FullName = fullName;
        Email = email;
        Birthdate = birthdate;
    }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime Birthdate { get; private set; }
}