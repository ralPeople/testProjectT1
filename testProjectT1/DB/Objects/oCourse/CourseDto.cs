using testProjectT1.DB.Objects.oStudent;

namespace testProjectT1.DB.Objects.oCourse
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();  

    }
}
