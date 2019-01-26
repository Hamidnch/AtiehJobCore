using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class StreetConfig : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.ToTable("Streets").HasKey(c => c.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CityCode).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => City and Street
            builder.HasOne(co => co.City)
                .WithMany(p => p.Streets)
                .HasForeignKey(p => p.CityCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
