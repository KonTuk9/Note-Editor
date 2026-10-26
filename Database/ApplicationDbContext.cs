using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Noteeditor.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;

        public string DbPath { get; }

        private const string c_dbName = "NoteDb.db";

        public ApplicationDbContext()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, c_dbName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Note>().HasKey(e => e.Id);
        }
    }
}
