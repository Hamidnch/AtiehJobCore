using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class EssentialPhoneConfig : IEntityTypeConfiguration<EssentialPhone>
    {
        public void Configure(EntityTypeBuilder<EssentialPhone> builder)
        {
            builder.ToTable("JobseekerEssentialPhones").HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Family).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Relationship).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(c => c.JobseekerId).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and EssentialPhone
            builder.HasOne(j => j.Jobseeker).
                WithMany(j => j.EssentialPhones).HasForeignKey(ep => ep.JobseekerId);
        }
    }
}