using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using Moq;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProjectId()
    {
        var createProjectCommand = new CreateProjectCommand
        {
            Title = "Title Test",
            Description = "Nice desc", TotalCost = 10000,
            IdClient = 1,
            IdFreelancer = 2
        };
        
        var projectRepository = new Mock<IProjectRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        unityOfWork.SetupGet(p => p.Projects).Returns(projectRepository.Object);
        var createProjectCommandHandler = new CreateProjectCommandHandler(unityOfWork.Object);
        
        var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());
        
        Assert.True(id >= 0);
        
        projectRepository.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
    }
    
}