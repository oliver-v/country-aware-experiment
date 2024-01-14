using Experiment.Domain;

namespace Experiment.API.Models;

public record CreateCustomerRequestDto
{
    public string Name { get; init; }
    public string IdCode { get; init; }
    public Gender Gender { get; init; }
}