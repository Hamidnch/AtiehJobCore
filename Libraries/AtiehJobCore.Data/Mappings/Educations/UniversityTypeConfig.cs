using AtiehJobCore.Domain.Entities.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Educations
{
    public class UniversityTypeConfig : IEntityTypeConfiguration<UniversityType>
    {
        public void Configure(EntityTypeBuilder<UniversityType> builder)
        {
            builder.ToTable("UniversityTypes").HasKey(c => c.Id);
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}