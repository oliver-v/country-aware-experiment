namespace Experiment.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IdCode { get; set; }
    public Gender Gender { get; set; }
    public Country Country { get; set; }
    
    public ICollection<Contact> Contacts { get; set; } = null!;
}