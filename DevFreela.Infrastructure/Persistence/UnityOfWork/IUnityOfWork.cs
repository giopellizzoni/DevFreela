using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence.UnityOfWork;

public interface IUnityOfWork
{
    IProjectRepository Projects { get; }
    IUserRepository Users { get; }
    ISkillRepository Skills { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task<int> SaveChangesAsync();
}