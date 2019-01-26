using AtiehJobCore.Domain.Entities.JobOpportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.JobOpportunities
{
    public class JobOpportunityConfig : IEntityTypeConfiguration<JobOpportunity>
    {

        public void Configure(EntityTypeBuilder<JobOpportunity> builder)
        {
            builder.ToTable("JobOpportunities").HasKey(j => j.Id);
            builder.Property(j => j.IdentityNumber).HasMaxLength(20);
            builder.Property(j => j.EnrollDate).HasColumnType("date");
            builder.Property(j => j.RepeatDate).HasColumnType("date");
            builder.Property(j => j.StartTime).HasMaxLength(5);
            builder.Property(j => j.EndTime).HasMaxLength(5);
            builder.Property(j => j.Applicant).HasMaxLength(100);
            builder.Property(j => j.ApplicantPost).HasMaxLength(30);
            builder.Property(j => j.InterviewPhone).HasMaxLength(50);
            builder.Property(j => j.EmployerAddressList).HasMaxLength(255);
            builder.Property(j => j.CurrentState).HasMaxLength(255);

            //  => OccupationalGroup and JobOpportunity
            builder.HasOne(c => c.OccupationalGroup)
               .WithMany(c => c.JobOpportunities)
               .HasForeignKey(j => j.OccupationalGroupCode);

            //  => OccupationalTitle and JobOpportunity
            builder.HasOne(c => c.OccupationalTitle)
               .WithMany(c => c.JobOpportunities)
               .HasForeignKey(j => j.OccupationalTitleCode);

            //  => Employer and JobOpportunity
            builder.HasOne(co => co.Employer)
               .WithMany(p => p.JobOpportunities)
               .HasForeignKey(p => p.EmployerCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => EmployerAddress and JobOpportunity
            builder.HasOne(co => co.InterviewEmployerAddress)
               .WithMany(p => p.JobOpportunities)
               .HasForeignKey(p => p.InterviewAddressCode)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
