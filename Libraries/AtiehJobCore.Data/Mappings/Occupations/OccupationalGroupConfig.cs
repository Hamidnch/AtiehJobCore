using AtiehJobCore.Domain.Entities.Occupations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Occupations
{
    public class OccupationalGroupConfig : IEntityTypeConfiguration<OccupationalGroup>
    {
        public void Configure(EntityTypeBuilder<OccupationalGroup> builder)
        {
            builder.ToTable("OccupationalGroups").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}