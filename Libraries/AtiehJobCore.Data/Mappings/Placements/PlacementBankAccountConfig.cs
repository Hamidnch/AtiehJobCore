using AtiehJobCore.Domain.Entities.Placements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Placements
{
    public class PlacementBankAccountConfig : IEntityTypeConfiguration<PlacementBankAccount>
    {

        public void Configure(EntityTypeBuilder<PlacementBankAccount> builder)
        {
            builder.ToTable("PlacementBankAccounts").HasKey(p => p.Id);
            builder.Property(p => p.AccountNumber).HasMaxLength(30);
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => Placement and PlacementBankAccount
            builder.HasOne(j => j.Placement)
               .WithMany(j => j.PlacementBankAccounts)
               .HasForeignKey(j => j.PlacementCode);

            // => PlacementBankAddress and Bank
            builder.HasOne(c => c.Bank)
               .WithMany(c => c.PlacementBankAccounts)
               .HasForeignKey(c => c.BankCode);
        }
    }
}
