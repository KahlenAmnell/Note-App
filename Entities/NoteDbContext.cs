using Microsoft.EntityFrameworkCore;

namespace Note_App_API.Entities
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Dashboard> Users { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
