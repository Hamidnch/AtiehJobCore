using AtiehJobCore.Domain.Entities.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Educations
{
    public class ScholarshipDisciplineConfig : IEntityTypeConfiguration<ScholarshipDiscipline>
    {
        public void Configure(EntityTypeBuilder<ScholarshipDiscipline> builder)
        {
            builder.ToTable("ScholarshipDisciplines").HasKey(c => c.Id);
            builder.Property(sd => sd.Title).IsRequired();
            builder.Property(sd => sd.Description).HasMaxLength(255);
        }
    }
}