using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence;

public class UnitOfWork : IUnityOfWork
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

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}