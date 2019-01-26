using AtiehJobCore.Domain.Entities.Placements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Placements
{
    public class PlacementConfig : IEntityTypeConfiguration<Placement>
    {
        public void Configure(EntityTypeBuilder<Placement> builder)
        {
            builder.ToTable("Placements").HasKey(b => b.Id);
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.LicenseLocation).HasMaxLength(50);
            builder.Property(b => b.LicenseNumber).HasMaxLength(16);
            builder.Property(b => b.ManagerName).HasMaxLength(50);
            builder.Property(p => p.MobileNumber).HasMaxLength(15);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(b => b.ManagerNationalCode).HasMaxLength(10);
            builder.Property(b => b.Name).HasMaxLength(50);
            builder.Property(b => b.WorkshopCode).HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(255);

            // Set concurrency token for entity
            builder.Property(j => j.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            //  => User and Placement
            builder.HasOne(j => j.User)
               .WithMany(j => j.Placements)
               .HasForeignKey(j => j.UserId);

            //  => Province and Placement
            builder.HasOne(j => j.Province)
               .WithMany(j => j.Placements)
                   .HasForeignKey(j => j.ProvinceCode);
        }
    }
}
