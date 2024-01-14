using Experiment.Domain.Entities;

namespace Experiment.Application.Rules.Validation;

public class CustomerValidationResult
{
    public bool Success { get; set; } = true;
    public ICollection<string?> FailedRules { get; set; } = new List<string?>();
    public string Message { get; set; }
    public Customer ValidatedCustomer { get; set; }
}