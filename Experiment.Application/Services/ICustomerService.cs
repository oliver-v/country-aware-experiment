using Experiment.Application.Models;
using Experiment.Domain.Entities;

namespace Experiment.Application.Services;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(CustomerModel model);
}