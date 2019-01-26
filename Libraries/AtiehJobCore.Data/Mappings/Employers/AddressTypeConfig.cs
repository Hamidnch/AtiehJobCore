using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> builder)
        {
            builder.ToTable("EmployerAddressTypes").HasKey(c => c.Id);
            builder.Property(c => c.Type).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
