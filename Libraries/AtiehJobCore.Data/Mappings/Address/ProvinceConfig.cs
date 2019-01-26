using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class ProvinceConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces").HasKey(c => c.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CountryCode).IsRequired();
            builder.Property(sp => sp.Abbreviation).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => Country and Province
            builder.HasOne(co => co.Country)
                .WithMany(p => p.Provinces)
                .HasForeignKey(p => p.CountryCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}