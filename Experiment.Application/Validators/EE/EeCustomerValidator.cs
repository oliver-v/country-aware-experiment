using Experiment.Application.Rules.Validation.EE;
using Experiment.Application.Services;

namespace Experiment.Application.Validators.EE;

public class EeCustomerValidator : CustomerValidator
{
    public EeCustomerValidator()
    {
        Rules.Add(new HasCorrectGenderRule());
    }
}