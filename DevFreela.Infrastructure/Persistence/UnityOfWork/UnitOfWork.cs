using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence.UnityOfWork;

public sealed class UnitOfWork : IUnityOfWork
{
    private readonly DevFreelaDbContext _context;
    private IDbContextTransaction _transaction;
    public UnitOfWork(DevFreelaDbContext context, IProjectRepository projects, IUserRepository users)
    {
        _context = context;
        Projects = projects;
        Users = users;
    }

    public IProjectRepository Projects { get; }
    public IUserRepository Users { get; }
    public ISkillRepository Skills { get; set; }
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await _transaction.RollbackAsync();
            throw e;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}