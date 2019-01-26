using AtiehJobCore.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Common
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments").HasKey(a => a.Id);
            builder.Property(a => a.FileName).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.FileName).HasMaxLength(100);
            builder.Property(a => a.ContentType).HasMaxLength(50);
            builder.Property(a => a.Extensions).HasMaxLength(10);
            builder.Property(a => a.Description).HasMaxLength(255);

            // => User and Attachment
            builder.HasOne(c => c.User)
               .WithMany(c => c.Attachments)
               .HasForeignKey(c => c.UserId);
        }
    }
}
