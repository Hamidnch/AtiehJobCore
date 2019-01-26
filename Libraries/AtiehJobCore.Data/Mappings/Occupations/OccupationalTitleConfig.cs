using AtiehJobCore.Domain.Entities.Occupations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Occupations
{
    public class OccupationalTitleConfig : IEntityTypeConfiguration<OccupationalTitle>
    {
        public void Configure(EntityTypeBuilder<OccupationalTitle> builder)
        {
            builder.ToTable("OccupationalTitles").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => OccupationalGroup and OccupationalTitle
            builder.HasOne(j => j.OccupationalGroup)
                .WithMany(j => j.OccupationalTitles)
                .HasForeignKey(fl => fl.OccupationalGroupCode)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}