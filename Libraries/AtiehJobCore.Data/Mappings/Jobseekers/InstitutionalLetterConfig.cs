using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class InstitutionalLetterConfig : IEntityTypeConfiguration<InstitutionalLetter>
    {
        public void Configure(EntityTypeBuilder<InstitutionalLetter> builder)
        {
            builder.ToTable("InstitutionalLetters").HasKey(c => c.Id);
            builder.Property(c => c.From).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
