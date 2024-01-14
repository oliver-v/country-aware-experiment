using Experiment.Application.Models;

namespace Experiment.Application.Rules.Validation;

public interface IValidationRule
{
    bool IsValid(CustomerModel model);
}