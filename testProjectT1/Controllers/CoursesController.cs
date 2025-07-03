using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testProjectT1.DB.Objects.oCourse;
using testProjectT1.DB.Objects.oStudent;
using testProjectT1.DB;

namespace testProjectT1.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourse request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Название курса не может быть пустым.");

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };


            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Created("", new { id = course.Id });
            }
            catch
            {
                await transaction.RollbackAsync();


                return StatusCode(500, "Ошибка при создании курса");

            }

            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            List<Course> courses = await _context.Courses
                .Include(c => c.Students)
                .ToListAsync();

            var result = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Students = c.Students.Select(s => new StudentDto{
                    Id = s.Id,
                    FullNane = s.FullName,
                }).ToList()
            });

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            Course course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return StatusCode(404, "Курс не найден");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return NoContent();
            }
            catch
            {
                await transaction.RollbackAsync();


                return StatusCode(500, "Ошибка при удалении курса");

            }
        }
    }
}