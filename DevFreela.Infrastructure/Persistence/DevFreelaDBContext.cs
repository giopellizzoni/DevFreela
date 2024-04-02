using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDBContext
{
    public DevFreelaDBContext()
    {
        Projects = new List<Project>
        {
            new Project("Meu Project ASPNET Core1", "Minha Descrição do Project1", 1, 1, 10000),
            new Project("Meu Project ASPNET Core2", "Minha Descrição do Project2", 1, 1, 20000),
            new Project("Meu Project ASPNET Core3", "Minha Descrição do Project3", 1, 1, 30000)
        };

        Users = new List<User>
        {
            new User("Blondell Snell", "bsnell0@amazon.de", new DateTime(1982, 06, 10)),
            new User("Gillian Rulf", "grulf1@auda.org.au", new DateTime(2001, 10, 04)),
            new User("Amie Spirit", "aspirit2@issuu.com", new DateTime(1987, 09, 27))
        };
        Skills = new List<Skill>
        {
            new Skill("C#"), 
            new Skill("ASP.NET Core"), 
            new Skill("Entity Framework")
        };
    }

    public List<Project> Projects { get; set; }
    public List<User> Users { get; set; }
    public List<Skill> Skills { get; set; }
}