using Experiment.Application.Models;
using Experiment.Application.Validators.EE;
using Experiment.Domain.Entities;
using Experiment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Application.Services.EE;

public interface IEeCustomerService : ICustomerService;

public class EeCustomerService(IUnitOfWork uow) : IEeCustomerService
{
    private readonly EeCustomerValidator _validator = new();
    
    public async Task<Customer> CreateCustomerAsync(CustomerModel model)
    {
        var validationResult = _validator.Validate(model);
        if (!validationResult.Success)
        {
            throw new ArgumentException(validationResult.Message);
        }
       
        await using var transaction = await uow.BeginTransactionAsync();
        try
        {
            uow.Customers.Add(validationResult.ValidatedCustomer);
            await uow.SaveChangesAsync();
            await uow.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await uow.RollbackTransactionAsync();
            throw new DbUpdateException(
                $"Failed to create EE customer", e);
        }
        
        return validationResult.ValidatedCustomer;
    }
}