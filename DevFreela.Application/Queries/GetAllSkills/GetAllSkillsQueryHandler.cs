using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler(ISkillRepository skillRepository)
    : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
{
 

    public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await skillRepository.GetAllAsync();
        return skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList() ;
    }
}