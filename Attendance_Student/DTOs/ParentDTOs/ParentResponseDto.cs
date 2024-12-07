namespace Attendance_Student.DTOs.ParentDTOs
{
    public class ParentResponseDto
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> StudentIds { get; set; }
    }
}
