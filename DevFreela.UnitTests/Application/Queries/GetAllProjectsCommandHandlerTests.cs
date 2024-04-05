using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests
{
    [Fact]
    public async Task ThreeProjectExists_Executed_ReturnThreeProjectViewModels()
    {
        var projects = new List<Project>
        {
            new("Test Name1", "Test Description", 1, 2, 1000),
            new("Test Name2", "Test Description", 1, 2, 1000),
            new("Test Name3", "Test Description", 1, 2, 1000)
        };

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

        var getAllProjectsQuery = new GetAllProjectsQuery("");
        var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

        var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, 
            new CancellationToken());
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(projects.Count, projectViewModelList.Count);
        
        projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
    }
}