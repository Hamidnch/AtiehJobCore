using AtiehJobCore.Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Address
{
    public class SectionConfig : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections").HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.ShahrestanCode).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Shahrestan and Section
            builder.HasOne(co => co.Shahrestan)
                .WithMany(p => p.Sections)
                .HasForeignKey(p => p.ShahrestanCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}