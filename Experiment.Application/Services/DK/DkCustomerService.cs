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

        await uow.ExecuteInTransactionAsync(async () =>
        {
            var customer = new Customer
            {
                Name = model.Name,
                Country = Country.DK,
                IdCode = model.IdCode,
                Contacts = new List<Contact>
                {
                    new() { ContactType = "email", ContactValue = "test@test.com" }
                }
            };

            uow.Customers.Add(customer);
            await uow.SaveChangesAsync();
        });
        
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