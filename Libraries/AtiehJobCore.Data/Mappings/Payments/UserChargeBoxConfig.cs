using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class UserChargeBoxConfig : IEntityTypeConfiguration<UserChargeBox>
    {
        public void Configure(EntityTypeBuilder<UserChargeBox> builder)
        {
            builder.ToTable("UserChargeBox");
            builder.Property(b => b.Description).HasMaxLength(255);
        }
    }
}
