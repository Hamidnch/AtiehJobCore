using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class PaymentGatewayConfig : IEntityTypeConfiguration<PaymentGateway>
    {

        public void Configure(EntityTypeBuilder<PaymentGateway> builder)
        {
            builder.ToTable("PaymentGateways").HasKey(pg => pg.Id);
            builder.Property(pg => pg.Name).IsRequired();
            builder.Property(pg => pg.Name).HasMaxLength(300);
            builder.Property(pg => pg.Description).HasMaxLength(255);

            // => PaymentGateway and Bank
            builder.HasOne(c => c.Bank)
               .WithMany(c => c.PaymentGateways)
               .HasForeignKey(c => c.BankCode);
        }
    }
}
