using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.AttendanceDTOs
{
    public class TeacherAttendanceDTO
    {
        [Required]
        public string TeacherId { get; set; }
    }
}
