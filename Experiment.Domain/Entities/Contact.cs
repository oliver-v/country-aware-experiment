namespace Experiment.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; }
    
    public string ContactValue { get; set; }
    public string ContactType { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}