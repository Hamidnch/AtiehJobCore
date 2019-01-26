using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payments)).HasKey(b => b.Id);
            builder.Property(b => b.OrderId).HasMaxLength(12);
            builder.Property(b => b.PaymentNumber).HasMaxLength(50);
            builder.Property(b => b.ReferenceId).HasMaxLength(50);
            builder.Property(b => b.UserIp).HasMaxLength(15);
            builder.Property(b => b.Token).HasMaxLength(100);
            builder.Property(b => b.Description).HasMaxLength(255);
            builder.Property(b => b.Description).HasMaxLength(255);

            // => PaymentGateway and Payment
            builder.HasOne(c => c.PaymentGateway)
               .WithMany(c => c.Payments)
               .HasForeignKey(c => c.PaymentGatewayCode);

            // => Payment and AmountUnit
            builder.HasOne(c => c.AmountUnit)
               .WithMany(c => c.Payments)
               .HasForeignKey(c => c.AmountUnitCode);
        }
    }
}
