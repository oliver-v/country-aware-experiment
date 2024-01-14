using Experiment.Application.Models;
using Experiment.Domain;

namespace Experiment.Application.Rules.Validation.EE;

public class HasCorrectGenderRule : IValidationRule
{
    public bool IsValid(CustomerModel model)
    {
        return model.Gender == ExpectedGender(model.IdCode);
    }

    private static Gender ExpectedGender(string idCode) =>
        idCode.First() == '4' ? Gender.FEMALE : Gender.MALE;
}