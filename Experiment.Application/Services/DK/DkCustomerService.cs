using Experiment.Application.Models;
using Experiment.Application.Validators.DK;
using Experiment.Domain;
using Experiment.Domain.Entities;
using Experiment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Application.Services.DK;

public interface IDkCustomerService : ICustomerService; 

public class DkCustomerService(IUnitOfWork uow) : IDkCustomerService
{
    private readonly DkCustomerValidator _validator = new();
    
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
                $"Failed to create DK customer", e);
        }
        
        return validationResult.ValidatedCustomer;
    }
    
    public async Task<Customer?> GetCustomerAsync(string idCode)
    {
        var customersWithContacts = await uow.Customers
            .GetAll(x => x.Country == Country.DK && x.IdCode == "123")
            .Include(x => x.Contacts)
            .SingleOrDefaultAsync();

        return customersWithContacts;
    }
}