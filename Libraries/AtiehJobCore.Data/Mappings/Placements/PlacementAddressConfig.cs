using AtiehJobCore.Domain.Entities.Placements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Placements
{
    public class PlacementAddressConfig : IEntityTypeConfiguration<PlacementAddress>
    {
        public void Configure(EntityTypeBuilder<PlacementAddress> builder)
        {
            builder.ToTable("PlacementAddresses").HasKey(p => p.Id);
            builder.Property(p => p.Address).HasMaxLength(255);
            builder.Property(p => p.Fax).HasMaxLength(25);
            builder.Property(p => p.Phone1).HasMaxLength(100);
            builder.Property(p => p.Phone2).HasMaxLength(100);
            builder.Property(p => p.PostalCode).HasMaxLength(25);
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => Placement and PlacementAddress
            builder.HasOne(j => j.Placement)
               .WithMany(j => j.PlacementAddresses)
               .HasForeignKey(j => j.PlacementCode);

            //  => Country and PlacementAddress
            builder.HasOne(c => c.Country)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.CountryCode);

            //  => Province and PlacementAddress
            builder.HasOne(c => c.Province)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.ProvinceCode);

            //  => Shahrestan and PlacementAddress
            builder.HasOne(c => c.Shahrestan)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.ShahrestanCode);

            //  => City and PlacementAddress
            builder.HasOne(c => c.City)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.CityCode);

            //  => Section and PlacementAddress
            builder.HasOne(c => c.Section)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.SectionCode);

            //  => Section and PlacementAddress
            builder.HasOne(c => c.City)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.CityCode);

            //  => Section and PlacementAddress
            builder.HasOne(c => c.Street)
               .WithMany(c => c.PlacementAddresses)
               .HasForeignKey(j => j.StreetCode);
        }
    }
}