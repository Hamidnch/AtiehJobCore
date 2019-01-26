using AtiehJobCore.Domain.Entities.Identity.Plus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Identity.Plus
{
    public class DataProtectionKeysConfig : IEntityTypeConfiguration<DataProtectionKey>
    {

        public void Configure(EntityTypeBuilder<DataProtectionKey> builder)
        {
            builder.ToTable("DataProtectionKeys");
            builder.HasIndex(e => e.FriendlyName).IsUnique();
        }
    };
}