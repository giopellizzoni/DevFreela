using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ProjectViewModel> GetAll(string query)
    {
        var projects = _dbContext.Projects;
        var projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt)).ToList();
        return projectsViewModel;
    }

    public ProjectDetailsViewModel GetById(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        var projectsDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt
        );
        return projectsDetailsViewModel;
    }

    public int Create(NewProjectInputModel inputModel)
    {
        var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient,
            inputModel.IdFreelancer, inputModel.TotalCost);
        _dbContext.Projects.Add(project);
        return project.IdClient;
    }

    public void Update(UpdateProjectInputModel inputModel)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        project?.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
    }

    public void Delete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project?.Cancel();
    }

    #region Comments

    public void CreateComment(CreateCommentInputModel inputModel)
    {
        var comment = new ProjectComment(inputModel.Comment, inputModel.IdProject, inputModel.IdUser);
        _dbContext.Comments.Add(comment);
    }

    public void Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project?.Start();
    }

    public void Finish(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project?.Finish();
    }

    #endregion
}