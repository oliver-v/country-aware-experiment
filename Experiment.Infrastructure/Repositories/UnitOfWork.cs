using Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Experiment.Infrastructure.Repositories;

public interface IUnitOfWork
{
    IRepository<Customer> Customers { get; }
    Task<int> SaveChangesAsync();    
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CustomerDbContext _context;

    public UnitOfWork(CustomerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Customers = new Repository<Customer>(_context);
    }

    public IRepository<Customer> Customers { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
    
    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}
