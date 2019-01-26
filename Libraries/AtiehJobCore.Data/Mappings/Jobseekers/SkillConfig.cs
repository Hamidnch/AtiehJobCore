using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class SkillConfig : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("JobseekerSkills").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.JobseekerId).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(255);
            builder.Property(c => c.CollegeName).HasMaxLength(255);
            //builder.Property(c => c.EndPeriodDate).HasColumnType("date");
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and Skill
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.Skills)
                .HasForeignKey(fl => fl.JobseekerId);
        }
    }
}