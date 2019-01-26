using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class SpecialDiseaseConfig : IEntityTypeConfiguration<SpecialDisease>
    {
        public void Configure(EntityTypeBuilder<SpecialDisease> builder)
        {
            builder.ToTable("JobseekerSpecialDiseases").HasKey(c => c.Id);
            builder.Property(c => c.JobseekerId).IsRequired();
            builder.Property(c => c.DiseaseDescription).HasMaxLength(255);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and SpecialDisease
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.SpecialDiseases)
                .HasForeignKey(sd => sd.JobseekerId);

            // one-to-many or one SpecialDisease and SpecialDiseaseName
            builder.HasOne(t => t.SpecialDiseaseName)
                .WithMany(t => t.SpecialDiseases)
                .HasForeignKey(t => t.SpecialDiseaseNameCode);
        }
    }
}