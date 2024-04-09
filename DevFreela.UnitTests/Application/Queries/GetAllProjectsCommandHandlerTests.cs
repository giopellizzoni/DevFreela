using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests
{
    [Fact]
    public async Task ThreeProjectExists_Executed_ReturnThreeProjectViewModels()
    {
        var projects = new PaginationResult<Project>
        {
            Data = new List<Project>
            {
                new("Test Name1", "Test Description", 1, 2, 1000),
                new("Test Name2", "Test Description", 1, 2, 1000),
                new("Test Name3", "Test Description", 1, 2, 1000)
            }
        };

        var projectRepositoryMock = new Mock<IProjectRepository>();
        projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), 
            It.IsAny<int>()).Result).Returns(projects);

        var unityOfWork = new Mock<IUnityOfWork>();
        unityOfWork.SetupGet(u => u.Projects).Returns(projectRepositoryMock.Object);
            
        var getAllProjectsQuery = new GetAllProjectsQuery { Query = "", Page = 1};
        var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(unityOfWork.Object);


        var paginationProjectViewModelList = await getAllProjectsQueryHandler
            .Handle(getAllProjectsQuery, new CancellationToken());

        Assert.NotNull(paginationProjectViewModelList);
        Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

        projectRepositoryMock
            .Verify(pr =>
                    pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result,
                Times.Once);
    }
}