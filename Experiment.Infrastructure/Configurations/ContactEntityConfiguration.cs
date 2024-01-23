using Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContactEntityConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.ContactValue).IsRequired();
        builder.Property(x => x.ContactType).IsRequired();
        
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Contacts)
            .HasForeignKey(x => x.CustomerId);
    }
}