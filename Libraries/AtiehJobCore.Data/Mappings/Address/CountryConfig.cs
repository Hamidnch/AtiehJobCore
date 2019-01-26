using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries").HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.TwoLetterIsoCode).HasMaxLength(2);
            builder.Property(c => c.ThreeLetterIsoCode).HasMaxLength(3);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}