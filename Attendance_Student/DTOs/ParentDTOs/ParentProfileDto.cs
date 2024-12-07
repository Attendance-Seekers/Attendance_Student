namespace Attendance_Student.DTOs.ParentDTOs
{
    public class ParentProfileDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public List<ParentStudentDto> Students { get; set; }
    }
}
