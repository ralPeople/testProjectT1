using Microsoft.EntityFrameworkCore;
using testProjectT1.DB.Objects.Course;
using testProjectT1.DB.Objects.Student;
namespace testProjectT1.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
    }
}
