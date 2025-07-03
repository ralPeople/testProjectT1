using Microsoft.EntityFrameworkCore;
using testProjectT1.DB.Objects.oCourse;
using testProjectT1.DB.Objects.oStudent;
namespace testProjectT1.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
    }
}
