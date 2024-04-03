using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
{
    private readonly string? _connectionString = configuration.GetConnectionString("DevFreelaCs");

    public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            await sqlConnection.OpenAsync(cancellationToken);
            var script = "SELECT Id, Description From Skills";
            var skills = await sqlConnection.QueryAsync<SkillViewModel>(script);
            return skills.ToList();
        }
        
        // WITH EF CORE
        // var skills = _dbContext.Skills;
        // var skillsViewModel = skills
        //     .Select(s => new SkillViewModel(s.Id, s.Description))
        //     .ToList();
        // return skillsViewModel;
    }
}