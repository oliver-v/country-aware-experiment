using Experiment.Application.Models;
using Experiment.Application.Rules.Validation;
using Experiment.Application.Rules.Validation.Common;
using Experiment.Application.Validators;
using Experiment.Domain.Entities;

namespace Experiment.Application.Services;

public class CustomerValidator : ICustomerValidator
{
    protected readonly List<IValidationRule> Rules = new();

    protected CustomerValidator()
    {
        AddCommonRules();
    }
    
    public CustomerValidationResult Validate(CustomerModel model)
    {
        var result = new CustomerValidationResult();
        
        foreach (var rule in Rules)
        {
            if (!rule.IsValid(model))
            {
                result.FailedRules.Add(rule.GetType().Name);
                result.Success = false;
            }
        }

        if (!result.Success)
        {
            result.Message = $"Customer validation failed to rules: {string.Join(", ", result.FailedRules)}";
        }
        else
        {
            result.ValidatedCustomer = new Customer
            {
                Name = model.Name,
                IdCode = model.IdCode,
                Gender = model.Gender
            };
        }
        

        return result;
    }

    private void AddCommonRules()
    {
        Rules.Add(new NameNotNullRule());
    }
}