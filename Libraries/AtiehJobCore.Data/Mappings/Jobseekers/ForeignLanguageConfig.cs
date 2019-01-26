using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class ForeignLanguageConfig : IEntityTypeConfiguration<ForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<ForeignLanguage> builder)
        {
            builder.ToTable("JobseekerForeignLanguages").HasKey(c => c.Id);
            builder.Property(c => c.JobseekerId).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and ForeignLanguage
            builder.HasOne(j => j.Jobseeker).
                WithMany(j => j.ForeignLanguages)
                .HasForeignKey(fl => fl.JobseekerId);

            //  or one ForeignLanguage and ForeignLanguageName
            builder.HasOne(c => c.ForeignLanguageName).
                WithMany(c => c.ForeignLanguages).
                HasForeignKey(j => j.ForeignLanguageNameCode);

            //  or one ForeignLanguage and LanguageDegreeType
            builder.HasOne(c => c.LanguageDegreeType).
                WithMany(c => c.ForeignLanguages).
                HasForeignKey(j => j.LanguageDegreeTypeCode);
        }
    }
}