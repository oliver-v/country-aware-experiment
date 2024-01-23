using Experiment.Domain;
using Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Infrastructure.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCountryAsync(Country country, string idCode);
}

public class CustomerRepository(DbContext context) : Repository<Customer>(context), ICustomerRepository
{
    public async Task<Customer?> GetByCountryAsync(Country country, string idCode)
    {
        var customer = await Context.Set<Customer>()
            .Where(c => c.Country == country && c.IdCode == idCode)
            .FirstOrDefaultAsync();
        
        return customer;
    }
}