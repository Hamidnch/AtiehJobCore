using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class SpecialDiseaseNameConfig : IEntityTypeConfiguration<SpecialDiseaseName>
    {
        public void Configure(EntityTypeBuilder<SpecialDiseaseName> builder)
        {
            builder.ToTable("SpecialDiseaseNames").HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
