using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note_App_API.Entities;

namespace Note_App_API.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Notes)
                .WithOne(n => n.Author)
                .HasForeignKey(n => n.AuthorID);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
