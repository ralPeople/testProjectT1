using Microsoft.EntityFrameworkCore;
using testProjectT1.DB.Objects.oCourse;
using testProjectT1.DB.Objects.oStudent;
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
