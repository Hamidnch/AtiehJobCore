using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class ForeignLanguageNamesConfig : IEntityTypeConfiguration<ForeignLanguageName>
    {
        public void Configure(EntityTypeBuilder<ForeignLanguageName> builder)
        {
            builder.ToTable("ForeignLanguageNames").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
