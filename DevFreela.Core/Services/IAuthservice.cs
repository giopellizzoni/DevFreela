namespace DevFreela.Core.Services;

public interface IAuthservice
{
    string GenerateJwtToken(string? email, string? role);
    string ComputeSha256Hash(string? password);
}