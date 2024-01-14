using Experiment.Domain;

namespace Experiment.Application.Models;

public record CreateCustomerModel
{
    public string Name { get; init; }
    public string IdCode { get; init; }
    public Gender Gender { get; init; }
}