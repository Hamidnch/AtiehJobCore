using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class EmployerServiceConfig : IEntityTypeConfiguration<EmployerTransfer>
    {

        public void Configure(EntityTypeBuilder<EmployerTransfer> builder)
        {
            builder.ToTable("EmployerTransfers").HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Employer and EmployerService
            builder.HasOne(j => j.Employer)
               .WithMany(j => j.EmployerTransfers)
               .HasForeignKey(j => j.EmployerCode);

            //  => Country and EmployerTransfer
            builder.HasOne(c => c.Country)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.CountryCode);

            //  => Province and EmployerTransfer

            builder.HasOne(c => c.Province)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.ProvinceCode);
            //  => Shahrestan and EmployerTransfer

            builder.HasOne(c => c.Shahrestan)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.ShahrestanCode);
            //  => City and EmployerTransfer

            builder.HasOne(c => c.City)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.CityCode);
            //  => Section and EmployerTransfer

            builder.HasOne(c => c.Section)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.SectionCode);

            //  => Section and EmployerTransfer
            builder.HasOne(c => c.City)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.CityCode);

            //  => Section and EmployerTransfer
            builder.HasOne(c => c.Street)
               .WithMany(c => c.EmployerTransfers)
               .HasForeignKey(j => j.StreetCode);
        }
    }
}
