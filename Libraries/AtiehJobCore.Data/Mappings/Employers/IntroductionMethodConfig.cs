using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class IntroductionMethodConfig : IEntityTypeConfiguration<IntroductionMethod>
    {
        public void Configure(EntityTypeBuilder<IntroductionMethod> builder)
        {
            builder.ToTable("EmployerIntroductionMethods").HasKey(c => c.Id);
            builder.Property(c => c.Method).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
