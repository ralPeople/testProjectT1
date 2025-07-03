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
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Guid id, [FromBody] CreateStudent dto)
        {
            var courseExists = await _context.Courses.AnyAsync(course => course.Id == id);
            if (!courseExists)
                return NotFound($"Курс {id} не найден.");

            var student = new Student
            {
                FullName = dto.FullName,
                CourseId = id
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

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