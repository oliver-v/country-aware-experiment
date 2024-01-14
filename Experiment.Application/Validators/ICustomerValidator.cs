using Experiment.Application.Models;
using Experiment.Application.Rules.Validation;

namespace Experiment.Application.Validators;

public interface ICustomerValidator
{
    CustomerValidationResult Validate(CustomerModel customer);
}