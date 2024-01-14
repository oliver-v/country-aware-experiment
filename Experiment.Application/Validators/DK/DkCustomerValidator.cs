using Experiment.Application.Rules.Validation.DK;
using Experiment.Application.Services;

namespace Experiment.Application.Validators.DK;

public class DkCustomerValidator : CustomerValidator
{
    public DkCustomerValidator()
    {
        Rules.Add(new HasCorrectGenderRule());
    }
}