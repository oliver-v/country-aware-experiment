using Experiment.Application.Models;
using Experiment.Domain.Entities;
using Experiment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Application.Services.EE;

public interface IEeCustomerService : ICustomerService;

public class EeCustomerService(IUnitOfWork uow) : IEeCustomerService
{
    public async Task CreateCustomerAsync(CreateCustomerModel model)
    {
        var customer = new Customer
        {
            Name = model.Name, // ads
            IdCode = model.IdCode,
            Gender = model.Gender
        }; 

        await using var transaction = await uow.BeginTransactionAsync();
        try
        {
            uow.Customers.Add(customer);
            await uow.SaveChangesAsync();
            await uow.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await uow.RollbackTransactionAsync();
            throw new DbUpdateException(
                $"Failed to create EE customer", e);
        }
    }
}