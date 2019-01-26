using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class OccupationalHistoryConfig : IEntityTypeConfiguration<OccupationalHistory>
    {
        public void Configure(EntityTypeBuilder<OccupationalHistory> builder)
        {
            builder.ToTable("JobseekerOccupationalHistories").HasKey(c => c.Id);
            builder.Property(c => c.OccupationalGroupCode).IsRequired();
            builder.Property(c => c.OccupationalTitleCode).IsRequired();
            builder.Property(c => c.OccupationalGroupCode).IsRequired();
            builder.Property(c => c.OrganizationName).HasMaxLength(255);
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.Phone).HasMaxLength(100);
            builder.Property(c => c.LeaveWorkReason).HasMaxLength(255);
            builder.Property(c => c.Description).HasMaxLength(255);

            //  => Jobseeker and OccupationalHistory
            builder.HasOne(j => j.Jobseeker)
                .WithMany(j => j.OccupationalHistories)
                .HasForeignKey(fl => fl.JobseekerId);

            //  => OccupationalHistory and OccupationalGroup
            builder.HasOne(co => co.OccupationalGroup).
                WithMany(p => p.OccupationalHistories).
                HasForeignKey(p => p.OccupationalGroupCode);

            //  => OccupationalHistory and OccupationalTitle
            builder.HasOne(co => co.OccupationalTitle).
                WithMany(p => p.OccupationalHistories).
                HasForeignKey(p => p.OccupationalTitleCode);
        }
    }
}