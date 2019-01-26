using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities").HasKey(c => c.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.SectionCode).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => Section and City
            builder.HasOne(co => co.Section)
                .WithMany(p => p.Cities)
                .HasForeignKey(p => p.SectionCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}