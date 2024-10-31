using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note_App_API.Entities;

namespace Note_App_API.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(n => n.Title)
                .HasMaxLength(100);

            builder.Property(n => n.Content)
                .HasMaxLength(450);
        }
    }
}
