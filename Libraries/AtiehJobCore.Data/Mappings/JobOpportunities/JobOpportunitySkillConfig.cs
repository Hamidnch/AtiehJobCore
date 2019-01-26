using AtiehJobCore.Domain.Entities.JobOpportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.JobOpportunities
{
    public class JobOpportunitySkillConfig : IEntityTypeConfiguration<JobOpportunitySkill>
    {
        public void Configure(EntityTypeBuilder<JobOpportunitySkill> builder)
        {
            builder.ToTable("JobOpportunitySkills").HasKey(j => j.Id);

            //  => JobOpportunity and JobOpportunitySkills
            builder.HasOne(co => co.JobOpportunity)
               .WithMany(p => p.JobOpportunitySkills)
               .HasForeignKey(p => p.JobOpportunityCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => JobOpportunity and SkillCourse
            builder.HasOne(co => co.SkillCourse)
               .WithMany(p => p.JobOpportunitySkills)
               .HasForeignKey(p => p.SkillCourseCode)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
