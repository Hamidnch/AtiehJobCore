using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class PaymentSettingConfig : IEntityTypeConfiguration<PaymentSetting>
    {
        public void Configure(EntityTypeBuilder<PaymentSetting> builder)
        {
            builder.ToTable("PaymentSettings").HasKey(pg => pg.Id);
            builder.Property(pg => pg.Description).HasMaxLength(255);

            // => PaymentSetting and PaymentGateway
            builder.HasOne(c => c.PaymentGateway)
               .WithMany(c => c.PaymentSettings)
                   .HasForeignKey(c => c.PaymentGatewayId);
        }
    }
}
