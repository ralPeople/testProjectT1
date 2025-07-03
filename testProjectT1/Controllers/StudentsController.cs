using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testProjectT1.DB.Objects.oCourse;
using testProjectT1.DB;
using testProjectT1.DB.Objects.oStudent;

namespace testProjectT1.Controllers
{
    [ApiController]
    [Route("courses/{id:guid}/students")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StudentsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Guid id, [FromBody] CreateStudent request)
        {
            var courseExists = await context.Courses.AnyAsync(course => course.Id == id);
            if (!courseExists)
                return NotFound($"Курс {id} не найден.");

            var student = new Student
            {
                FullName = request.FullName,
                CourseId = id
            };

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.Students.Add(student);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Created("", new { id = student.Id });
            }
            catch
            {
                await transaction.RollbackAsync();


                return StatusCode(500, "Ошибка при создании студента");

            }
        }
    }
}