using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly string? _connectionString;

    public SkillRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        // WITH Dapper Sample
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            await sqlConnection.OpenAsync();
            var script = "SELECT Id, Description From Skills";
            var skills = await sqlConnection.QueryAsync<Skill>(script);
            return skills.ToList();
        }
        
        // WITH EF CORE Sample
        // var skills = _dbContext.Skills;
        // var skillsViewModel = skills
        //     .Select(s => new SkillViewModel(s.Id, s.Description))
        //     .ToList();
        // return skillsViewModel;
    }
}