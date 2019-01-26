using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class AmountUnitConfig : IEntityTypeConfiguration<AmountUnit>
    {

        public void Configure(EntityTypeBuilder<AmountUnit> builder)
        {
            builder.ToTable("AmountUnits").HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(50);
            builder.Property(b => b.Description).HasMaxLength(255);
        }
    }
}
