using AtiehJobCore.Domain.Entities.JobOpportunities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.JobOpportunities
{
    public class JobOpportunityReservationConfig : IEntityTypeConfiguration<JobOpportunityReservation>
    {
        public void Configure(EntityTypeBuilder<JobOpportunityReservation> builder)
        {
            builder.ToTable("JobOpportunityReservations").HasKey(j => j.Id);
            builder.Property(j => j.JobseekerCode).IsRequired();
            builder.Property(j => j.JobOpportunityCode).IsRequired();

            //  => JobOpportunityReservation and Jobseeker
            builder.HasOne(co => co.Jobseeker)
               .WithMany(p => p.JobOpportunityReservations)
               .HasForeignKey(p => p.JobseekerCode)
               .OnDelete(DeleteBehavior.Restrict);

            //  => JobOpportunityReservation and JobOpportunity
            builder.HasOne(co => co.JobOpportunity)
               .WithMany(p => p.JobOpportunityReservations)
               .HasForeignKey(p => p.JobOpportunityCode)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
