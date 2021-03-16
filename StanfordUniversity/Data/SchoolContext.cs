using StanfordUniversity.Models;
using Microsoft.EntityFrameworkCore;


namespace StanfordUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Students> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Groups>().HasOne(p => p.Courses).WithMany(f => f.Groups).HasForeignKey(p => p.CourseID).IsRequired();
            modelBuilder.Entity<Students>().HasOne(p => p.Groups).WithMany(f => f.Students).HasForeignKey(p => p.GroupID).IsRequired();

        }
    }
}