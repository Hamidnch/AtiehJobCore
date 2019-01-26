using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class VehicleNameConfig : IEntityTypeConfiguration<VehicleName>
    {
        public void Configure(EntityTypeBuilder<VehicleName> builder)
        {
            builder.ToTable("VehicleNames").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
