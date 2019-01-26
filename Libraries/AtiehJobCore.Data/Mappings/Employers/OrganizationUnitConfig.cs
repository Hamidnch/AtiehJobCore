using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class OrganizationUnitConfig : IEntityTypeConfiguration<OrganizationUnit>
    {
        public void Configure(EntityTypeBuilder<OrganizationUnit> builder)
        {
            builder.ToTable("EmployerOrganizationUnits").HasKey(c => c.Id);
            builder.Property(c => c.Type).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
