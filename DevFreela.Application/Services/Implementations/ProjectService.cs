using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionstring;
    public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _connectionstring = configuration.GetConnectionString("DevFreelaCs");
    }

    public List<ProjectViewModel> GetAll(string query)
    {
        var projects = _dbContext.Projects;
        var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();
        return projectsViewModel;
    }

    public ProjectDetailsViewModel? GetById(int id)
    {
        var project = _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefault(p => p.Id == id);
        
        if (project == null) return null;
        var projectsDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client.FullName,
            project.Freelancer.FullName
        );
        return projectsDetailsViewModel;
    }

    public int Create(NewProjectInputModel inputModel)
    {
        var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient,
            inputModel.IdFreelancer, inputModel.TotalCost);
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        return project.IdClient;
    }

    public void Update(UpdateProjectInputModel inputModel)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        project?.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        _dbContext.SaveChanges();
        project?.Cancel();
    }

    #region Comments

    public void CreateComment(CreateCommentInputModel inputModel)
    {
        var comment = new ProjectComment(inputModel.Comment, inputModel.IdProject, inputModel.IdUser);
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
    }

    public void Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project?.Start();
        // _dbContext.SaveChanges();
        using (var sqlConnection = new SqlConnection(_connectionstring))
        {
            sqlConnection.Open();
            var script = "UPDATE Project SET Status = @status, StartedAt = @startedat WHERE Id = @id";
            sqlConnection.Execute(script, new { status = project?.Status, startedat = project?.StartedAt, id });
            
        }
    }

    public void Finish(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project?.Finish();
        _dbContext.SaveChanges();
    }

    #endregion
}