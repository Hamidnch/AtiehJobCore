using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class ShahrestanConfig : IEntityTypeConfiguration<Shahrestan>
    {
        public void Configure(EntityTypeBuilder<Shahrestan> builder)
        {
            builder.ToTable("Shahrestans").HasKey(c => c.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ProvinceCode).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255);

            //  => Province and Shahrestan
            builder.HasOne(co => co.Province)
                .WithMany(p => p.Shahrestans)
                .HasForeignKey(p => p.ProvinceCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}