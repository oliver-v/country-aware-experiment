using Experiment.Application.Models;
using Experiment.Domain;
using Experiment.Domain.Entities;
using Experiment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Application.Services.DK;

public interface IDkCustomerService : ICustomerService; 

public class DkCustomerService(IUnitOfWork uow) : IDkCustomerService
{
    public async Task CreateCustomerAsync(CreateCustomerModel model)
    {
        var customer = new Customer
        {
            Name = model.Name,
            IdCode = model.IdCode,
            Gender = GetGender(model.IdCode)
        };
        
        await using var transaction = await uow.BeginTransactionAsync();
        try
        {
            uow.Customers.Add(customer);
            await uow.CommitTransactionAsync();
            await uow.SaveChangesAsync();
        }
        catch (Exception e)
        {
            await uow.RollbackTransactionAsync();
            throw new DbUpdateException(
                $"Failed to create DK customer", e);
        }
    }

    private static Gender GetGender(string idCode) => 
        idCode.Last() %2 == 1 ? Gender.MALE : Gender.FEMALE;
}