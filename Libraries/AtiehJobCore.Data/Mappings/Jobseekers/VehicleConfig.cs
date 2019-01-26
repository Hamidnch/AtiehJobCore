using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("JobseekerVehicles").HasKey(c => c.Id);
            builder.Property(c => c.VehicleModel).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and Vehicle
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.Vehicles)
                .HasForeignKey(x => x.JobseekerId);

            // Map one-to-many or one Vehicle and VehicleName
            builder.HasOne(t => t.VehicleName)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(t => t.VehicleNameCode);
        }
    }
}
