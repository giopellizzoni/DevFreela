using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests
{
    [Fact]
    public async Task ThreeProjectExists_Executed_ReturnThreeProjectViewModels()
    {
        var projects = new PaginationResult<Project>
        {
            Page = 1,
            TotalPages = 1,
            ItemsCount = 3,
            PageSize = 10,
            Data = new List<Project>
            {
                new("Test Name1", "Test Description", 1, 2, 1000),
                new("Test Name2", "Test Description", 1, 2, 1000),
                new("Test Name3", "Test Description", 1, 2, 1000)
            }
        };

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(pr => pr.GetAllAsync("", 1).Result).Returns(projects);

        var getAllProjectsQuery = new GetAllProjectsQuery();
        var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

        var paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery,
            new CancellationToken());
        
        
        
        Assert.NotNull(paginationProjectViewModelList);
        Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

        projectRepositoryMock
            .Verify(pr =>
                    pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result,
                Times.Once);
    }
}