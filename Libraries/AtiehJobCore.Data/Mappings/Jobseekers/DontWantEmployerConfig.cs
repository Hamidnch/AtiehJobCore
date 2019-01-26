using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class DontWantEmployerConfig : IEntityTypeConfiguration<DontWantEmployer>
    {
        public void Configure(EntityTypeBuilder<DontWantEmployer> builder)
        {
            builder.ToTable("JobseekerDontWantEmployers").HasKey(c => c.Id);
            builder.Property(c => c.OrganizationName).HasMaxLength(100);
            builder.Property(c => c.Phone1).HasMaxLength(50);
            builder.Property(c => c.Phone2).HasMaxLength(50);
            builder.Property(c => c.Cause).HasMaxLength(500);
            builder.Property(c => c.Description).HasMaxLength(1000);

            //  => Jobseeker and DontWantEmployer
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.DontWantEmployers)
                .HasForeignKey(x => x.JobseekerId);
        }
    }
}
