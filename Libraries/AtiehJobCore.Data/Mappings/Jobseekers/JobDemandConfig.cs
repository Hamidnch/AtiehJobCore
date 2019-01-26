using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class JobDemandConfig : IEntityTypeConfiguration<JobDemand>
    {
        public void Configure(EntityTypeBuilder<JobDemand> builder)
        {
            builder.ToTable("JobseekerJobDemands").HasKey(c => c.Id);
            builder.Property(c => c.OccupationalGroupCode).IsRequired();
            builder.Property(c => c.OccupationalTitleCode).IsRequired();
            builder.Property(c => c.JobseekerId).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and JobDemand
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.JobDemands)
                .HasForeignKey(fl => fl.JobseekerId);

            //  => JobDemand and OccupationalGroup
            builder
                .HasOne(co => co.OccupationalGroup)
                .WithMany(p => p.JobDemands)
                .HasForeignKey(p => p.OccupationalGroupCode);

            //  => JobDemand and OccupationalTitle
            builder.HasOne(co => co.OccupationalTitle)
                .WithMany(p => p.JobDemands)
                .HasForeignKey(p => p.OccupationalTitleCode);
        }
    }
}