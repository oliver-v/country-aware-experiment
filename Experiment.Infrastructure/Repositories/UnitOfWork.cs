using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Experiment.Infrastructure.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();    
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task ExecuteInTransactionAsync(Func<Task> action);
    
    ICustomerRepository Customers { get; }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CustomerDbContext _context;
    
    public ICustomerRepository Customers { get; }

    public UnitOfWork(CustomerDbContext context)
    {
        _context = context ?? throw new ArgumentNullException("DbContext is null");
        
        Customers = new CustomerRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
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
    
    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        await using var transaction = await BeginTransactionAsync();
        try
        {
            await action();
            await CommitTransactionAsync();
        }
        catch (Exception)
        {
            await RollbackTransactionAsync();
            throw;
        }
    }
}
