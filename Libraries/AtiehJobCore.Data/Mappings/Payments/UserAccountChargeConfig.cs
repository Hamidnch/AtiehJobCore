using AtiehJobCore.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Payments
{
    public class UserAccountChargeConfig : IEntityTypeConfiguration<UserAccountCharge>
    {
        public void Configure(EntityTypeBuilder<UserAccountCharge> builder)
        {
            builder.ToTable("UserAccountCharges");
            builder.Property(b => b.OrderId).HasMaxLength(12);
            builder.Property(b => b.Description).HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(255);

            //one - to - one => Payment and UserAccountCharge
            builder.HasOne(c => c.Payment)
               .WithMany(c => c.UserAccountCharges)
               .HasForeignKey(c => c.PaymentId);

            // => User and UserAccountCharge
            builder.HasOne(c => c.User)
               .WithMany(c => c.UserAccountCharges)
               .HasForeignKey(c => c.UserId);
        }
    }
}
