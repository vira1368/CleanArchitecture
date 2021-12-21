using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
