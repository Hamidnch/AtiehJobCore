using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class SkillDemandConfig : IEntityTypeConfiguration<SkillDemand>
    {
        public void Configure(EntityTypeBuilder<SkillDemand> builder)
        {
            builder.ToTable("JobseekerSkillDemands").HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and SkillDemand
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.SkillsDemands)
                .HasForeignKey(x => x.JobseekerId);

            //  => SkillDemand and SkillCourse
            builder.HasOne(t => t.SkillCourse)
                .WithMany(t => t.SkillDemands)
                .HasForeignKey(t => t.SkillCourseCode);
        }
    }
}
