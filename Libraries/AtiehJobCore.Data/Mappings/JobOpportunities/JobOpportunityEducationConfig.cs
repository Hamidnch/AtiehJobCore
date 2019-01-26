using AtiehJobCore.Domain.Entities.JobOpportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.JobOpportunities
{
    public class JobOpportunityEducationConfig : IEntityTypeConfiguration<JobOpportunityEducation>
    {
        public void Configure(EntityTypeBuilder<JobOpportunityEducation> builder)
        {
            builder.ToTable("JobOpportunityEducations").HasKey(j => j.Id);
            builder.Property(j => j.UniversityName).HasMaxLength(50);

            //  => EducationLevel and JobOpportunityEducation
            builder.HasOne(co => co.EducationLevel)
               .WithMany(p => p.JobOpportunityEducations)
               .HasForeignKey(p => p.EducationLevelCode)
                   .OnDelete(DeleteBehavior.Restrict);

            //  => ScholarshipDiscipline and JobOpportunityEducation
            builder.HasOne(co => co.ScholarshipDiscipline)
               .WithMany(p => p.JobOpportunityEducations)
               .HasForeignKey(p => p.ScholarshipDisciplineCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => ScholarshipTendency and JobOpportunityEducation
            builder.HasOne(co => co.ScholarshipTendency)
               .WithMany(p => p.JobOpportunityEducations)
               .HasForeignKey(p => p.ScholarshipTendencyCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => UniversityType and JobOpportunityEducation
            builder.HasOne(co => co.UniversityType)
               .WithMany(p => p.JobOpportunityEducations)
               .HasForeignKey(p => p.UniversityTypeCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => JobOpportunity and JobOpportunityEducation
            builder.HasOne(co => co.JobOpportunity)
               .WithMany(p => p.JobOpportunityEducations)
               .HasForeignKey(p => p.JobOpportunityCode)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
