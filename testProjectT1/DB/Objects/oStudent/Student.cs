using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testProjectT1.DB.Objects.oCourse;

namespace testProjectT1.DB.Objects.oStudent
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FullName { get; set; } = string.Empty;

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        public Course Course { get; set; } 
        

    }
}
