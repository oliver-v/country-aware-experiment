using Experiment.Application.Models;

namespace Experiment.Application.Services;

public interface ICustomerService
{
    Task CreateCustomerAsync(CreateCustomerModel model);
}