using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class EmployerAddressConfig : IEntityTypeConfiguration<EmployerAddress>
    {
        public void Configure(EntityTypeBuilder<EmployerAddress> builder)
        {
            builder.ToTable("EmployerAddresses").HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => AddressType and EmployerAddress
            builder.HasOne(c => c.AddressType)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.AddressTypeCode);

            //  => Employer and EmployerAddress
            builder.HasOne(j => j.Employer)
               .WithMany(j => j.EmployerAddresses)
               .HasForeignKey(j => j.EmployerCode);

            //  => Country and EmployerAddress
            builder.HasOne(c => c.Country)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.CountryCode);

            //  => Province and EmployerAddress
            builder.HasOne(c => c.Province)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.ProvinceCode);

            //  => Shahrestan and EmployerAddress
            builder.HasOne(c => c.Shahrestan)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.ShahrestanCode);

            //  => City and EmployerAddress
            builder.HasOne(c => c.City)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.CityCode);

            //  => Section and EmployerAddress
            builder.HasOne(c => c.Section)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.SectionCode);

            //  => Section and EmployerAddress
            builder.HasOne(c => c.City)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.CityCode);

            //  => Section and EmployerAddress
            builder.HasOne(c => c.Street)
               .WithMany(c => c.EmployerAddresses)
               .HasForeignKey(j => j.StreetCode);
        }
    }
}