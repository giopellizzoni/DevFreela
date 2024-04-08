namespace DevFreela.Core.Exceptions;

public class PasswordFieldEmptyException : Exception
{
    public PasswordFieldEmptyException() : base("Password is null or empty")
    {
    }
}