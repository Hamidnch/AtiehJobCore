using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class EducationConfig : IEntityTypeConfiguration<Education>
    {

        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.ToTable("JobseekerEducations").HasKey(c => c.Id);
            builder.Property(c => c.UniversityName).HasMaxLength(255);
            builder.Property(c => c.OtherProvinces).HasMaxLength(255);
            builder.Property(c => c.JobseekerCode).IsRequired();
            builder.Property(c => c.GraduationDate).HasColumnType("date");
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and Education
            builder.HasOne(co => co.Jobseeker)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.JobseekerCode)
                .OnDelete(DeleteBehavior.Restrict);

            //  => Education and ScholarshipDiscipline
            builder.HasOne(co => co.ScholarshipDiscipline)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.ScholarshipDisciplineCode)
                .OnDelete(DeleteBehavior.Restrict);

            //  => Education and ScholarshipTendency
            builder.HasOne(co => co.ScholarshipTendency)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.ScholarshipTendencyCode)
                .OnDelete(DeleteBehavior.Restrict);

            //  => Education and EducationLevel
            builder.HasOne(co => co.EducationLevel)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.EducationLevelCode)
                .OnDelete(DeleteBehavior.Restrict);

            //  => Education and UniversityType
            builder.HasOne(co => co.UniversityType)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.UniversityTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            //  => Education and Province
            builder.HasOne(co => co.Province)
                .WithMany(p => p.Educations)
                .HasForeignKey(p => p.ProvinceCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}