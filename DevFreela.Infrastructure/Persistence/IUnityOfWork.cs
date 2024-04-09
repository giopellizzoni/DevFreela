using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence;

public interface IUnityOfWork
{
    IProjectRepository Projects { get; }
    IUserRepository Users { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task<int> CompleteAsync();
}