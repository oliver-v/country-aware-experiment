using Experiment.Application.Models;

namespace Experiment.Application.Rules.Validation.Common;

public class NameNotNullRule : IValidationRule
{
    public bool IsValid(CustomerModel model)
    {
        return !string.IsNullOrEmpty(model.Name);
    }
}