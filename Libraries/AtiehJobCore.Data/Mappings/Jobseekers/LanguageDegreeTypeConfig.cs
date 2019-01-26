using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class LanguageDegreeTypeConfig : IEntityTypeConfiguration<LanguageDegreeType>
    {
        public void Configure(EntityTypeBuilder<LanguageDegreeType> builder)
        {
            builder.ToTable("LanguageDegreeTypes").HasKey(c => c.Id);
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
