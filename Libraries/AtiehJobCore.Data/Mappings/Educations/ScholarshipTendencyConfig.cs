using AtiehJobCore.Domain.Entities.Educations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Educations
{
    public class ScholarshipTendencyConfig : IEntityTypeConfiguration<ScholarshipTendency>
    {
        public void Configure(EntityTypeBuilder<ScholarshipTendency> builder)
        {
            builder.ToTable("ScholarshipTendencies").HasKey(c => c.Id);
            builder.Property(st => st.Title).IsRequired();
            builder.Property(st => st.Description).HasMaxLength(255);

            //  => ScholarshipDiscipline and ScholarshipTendencies
            builder.HasOne(j => j.ScholarshipDiscipline)
                .WithMany(j => j.ScholarshipTendencies)
                .HasForeignKey(fl => fl.ScholarshipDisciplineCode);
        }
    }
}
