using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class DrivingLicenseConfig : IEntityTypeConfiguration<DrivingLicense>
    {
        public void Configure(EntityTypeBuilder<DrivingLicense> builder)
        {
            builder.ToTable("JobseekerDrivingLicenses").HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and DrivingLicense
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.DrivingLicenses)
                .HasForeignKey(x => x.JobseekerId);

            // Map one-to-many or one DrivingLicense and DrivingLicenseName
            builder.HasOne(t => t.DrivingLicenseName)
                .WithMany(t => t.DrivingLicenses)
                .HasForeignKey(t => t.DrivingLicenseNameCode);
        }
    }
}
