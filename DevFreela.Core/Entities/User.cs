namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    public User(string? fullName, string? email, DateTime birthdate, string? password, string? role)
    {
        FullName = fullName;
        Email = email;
        Birthdate = birthdate;
        Password = password;
        Role = role;
        Active = true;
        
        CreatedAt = DateTime.Now;
        Skills = new List<UserSkill>();
        OwnedProjects = new List<Project>();
        FreelanceProjects = new List<Project>();
        Comments = new List<ProjectComment>();
    }

    public string? FullName { get; private set; }
    public string? Email { get; private set; }
    public DateTime Birthdate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Active { get; private set; }

    public string? Password { get; private set; }
    public string? Role { get; private set; }
    
    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }
}