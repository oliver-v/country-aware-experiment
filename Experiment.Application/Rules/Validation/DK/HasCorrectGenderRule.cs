using Experiment.Application.Models;
using Experiment.Domain;

namespace Experiment.Application.Rules.Validation.DK;

public class HasCorrectGenderRule : IValidationRule
{
    public bool IsValid(CustomerModel model)
    {
        return model.Gender == ExpectedGender(model.IdCode);
    }

    private static Gender ExpectedGender(string idCode) =>
        long.Parse(idCode) % 2 == 0 ? Gender.FEMALE : Gender.MALE;
}