using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations;

public class SkillService : ISkillService
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionstring;
    public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionstring = configuration.GetConnectionString("DevFreelaCs");
    }
    
    public List<SkillViewModel> GetAll()
    {
        using (var sqlConnection = new SqlConnection(_connectionstring))
        {
            sqlConnection.Open();
            var script = "SELECT Id, Description From Skills";
            return sqlConnection.Query<SkillViewModel>(script).ToList();
        }
        // var skills = _dbContext.Skills;
        // var skillsViewModel = skills
        //     .Select(s => new SkillViewModel(s.Id, s.Description))
        //     .ToList();
        // return skillsViewModel;
    }
}