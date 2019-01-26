using AtiehJobCore.Domain.Entities.Employers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Employers
{
    public class EmployerConfig : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employers").HasKey(c => c.Id);
            builder.Property(c => c.HumanApplicantUnit).HasMaxLength(255);
            builder.Property(c => c.EnrollTime).HasMaxLength(5);
            builder.Property(c => c.ActivityType).HasMaxLength(255);
            builder.Property(c => c.UnitCode).HasMaxLength(50);
            builder.Property(c => c.NationalCode).HasMaxLength(10);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Family).HasMaxLength(50);
            builder.Property(c => c.InsuranceCode).HasMaxLength(100);
            builder.Property(c => c.Phone).HasMaxLength(255);
            builder.Property(c => c.MobileNumber).HasMaxLength(11);
            builder.Property(c => c.Description).HasMaxLength(255);

            // Set concurrency token for entity
            builder.Property(j => j.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // => User and Employer
            builder.HasOne(c => c.User)
                .WithMany(c => c.Employers)
                .HasForeignKey(c => c.UserId);

            //  => OrganizationUnit and Employer
            builder.HasOne(c => c.OrganizationUnit)
                .WithMany(c => c.Employers)
                .HasForeignKey(j => j.OrganizationUnitCode);

            //  => IntroductionMethod and Employer
            builder.HasOne(c => c.IntroductionMethod)
                .WithMany(c => c.Employers)
                .HasForeignKey(j => j.IntroductionMethodCode);
        }
    }
}
