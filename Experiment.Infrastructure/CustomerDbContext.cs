using Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Infrastructure;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
         
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
    }
}