using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class DrivingLicenseNameConfig : IEntityTypeConfiguration<DrivingLicenseName>
    {
        public void Configure(EntityTypeBuilder<DrivingLicenseName> builder)
        {
            builder.ToTable("DrivingLicenseNames").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
