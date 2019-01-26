using AtiehJobCore.Domain.Entities.Identity.Plus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtiehJobCore.Data.Mappings.Identity.Plus
{
    public class UserUsedPasswordsConfig : IEntityTypeConfiguration<UserUsedPassword>
    {
        public void Configure(EntityTypeBuilder<UserUsedPassword> builder)
        {
            builder.Property(up => up.HashedPassword)
                .HasMaxLength(450)
                .IsRequired();
            builder.HasOne(up => up.User)
                .WithMany(u => u.UserUsedPasswords);

            builder.ToTable("UserUsedPasswords");
        }
    }
}