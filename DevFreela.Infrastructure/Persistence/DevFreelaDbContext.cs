using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext
{
    public DevFreelaDbContext()
    {
        Projects = new List<Project>
        {
            new("Meu Project ASPNET Core1", "Minha Descrição do Project1", 1, 1, 10000),
            new("Meu Project ASPNET Core2", "Minha Descrição do Project2", 1, 1, 20000),
            new("Meu Project ASPNET Core3", "Minha Descrição do Project3", 1, 1, 30000)
        };

        Users = new List<User>
        {
            new("Blondell Snell", "bsnell0@amazon.de", new DateTime(1982, 06, 10)),
            new("Gillian Rulf", "grulf1@auda.org.au", new DateTime(2001, 10, 04)),
            new("Amie Spirit", "aspirit2@issuu.com", new DateTime(1987, 09, 27))
        };
        Skills = new List<Skill>
        {
            new("C#"), 
            new("ASP.NET Core"), 
            new("Entity Framework")
        };
    }

    public List<Project> Projects { get; set; }
    public List<User> Users { get; set; }
    public List<Skill> Skills { get; set; }
    public List<ProjectComment> Comments { get; set; }
}