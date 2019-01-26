using AtiehJobCore.Domain.Entities.Jobseekers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Jobseekers
{
    public class JobseekerConfig : IEntityTypeConfiguration<Jobseeker>
    {
        public void Configure(EntityTypeBuilder<Jobseeker> builder)
        {
            builder.ToTable("Jobseekers").HasKey(j => j.Id);//.HasKey(j => j.FileNumber);

            builder.Property(j => j.FileNumber).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.FileNumber).IsUnique();
            //.HasColumnAnnotation(
            //    IndexAnnotation.AnnotationName,
            //    new IndexAnnotation(
            //        new IndexAttribute("IX_JobseekerFileNumber", 2) { IsUnique = false }));

            builder.Property(j => j.FileNumber).HasMaxLength(50);
            builder.Property(j => j.Name).HasMaxLength(50);
            builder.Property(j => j.Family).HasMaxLength(50);
            builder.Property(j => j.FatherName).HasMaxLength(50);
            builder.Property(j => j.BirthPlace).HasMaxLength(50);
            builder.Property(j => j.Email).HasMaxLength(255);
            builder.Property(j => j.MobileNumber).HasMaxLength(20);
            builder.Property(j => j.Phone).HasMaxLength(100);
            builder.Property(j => j.NationalCode).HasMaxLength(10);
            builder.Property(j => j.ExemptionCase).HasMaxLength(255);
            builder.Property(j => j.HistoryAbuseDescription).HasMaxLength(255);
            builder.Property(j => j.InsuranceNumber).HasMaxLength(50);
            builder.Property(j => j.PostalCode).HasMaxLength(50);
            builder.Property(j => j.Picture).HasMaxLength(255);
            builder.Property(j => j.ReligionName).HasMaxLength(50);
            builder.Property(j => j.RetirementPlace).HasMaxLength(100);
            builder.Property(j => j.CurrentState).HasMaxLength(255);
            builder.Property(j => j.SsnId).HasMaxLength(50);
            builder.Property(j => j.SsnSerial).HasMaxLength(50);
            builder.Property(j => j.WorkTime).HasMaxLength(7);
            builder.Property(j => j.EnrollTime).HasMaxLength(5);
            builder.Property(j => j.EnrollDate).HasColumnType("date");
            builder.Property(j => j.DateOfBirth).HasColumnType("date");
            builder.Property(j => j.StartDatePension).HasColumnType("date");
            builder.Property(j => j.EndDatePension).HasColumnType("date");
            builder.Property(j => j.Description).HasMaxLength(255);

            //  => Country and Jobseeker
            builder.HasOne(c => c.Country)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.CountryCode);
            //  => Province and Jobseeker
            builder.HasOne(c => c.Province)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.ProvinceCode);
            //  => Shahrestan and Jobseeker
            builder.HasOne(c => c.Shahrestan)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.ShahrestanCode);
            //  => Section and Jobseeker
            builder.HasOne(c => c.Section)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.SectionCode);
            //  => City and Jobseeker
            builder.HasOne(c => c.City)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.CityCode);
            //  => Street and Jobseeker
            builder.HasOne(c => c.Street)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.StreetCode);
            //  => InstitutionalLetter and Jobseeker
            builder.HasOne(c => c.InstitutionalLetter)
                .WithMany(c => c.Jobseekers)
                .HasForeignKey(j => j.InstitutionalLetterCode);

            // => User and Jobseeker
            builder.HasOne(c => c.User).
                WithMany(c => c.Jobseekers).
                HasForeignKey(c => c.UserId);
        }
    }
}