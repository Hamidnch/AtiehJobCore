using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class SkillCourseConfig : IEntityTypeConfiguration<SkillCourse>
    {
        public void Configure(EntityTypeBuilder<SkillCourse> builder)
        {
            builder.ToTable("SkillCourses").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
