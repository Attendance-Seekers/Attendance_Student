using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.AttendanceDTOs
{
    public class AttendanceDTO
    {
        [Required]
        public DateOnly DateAttendance { get; set; } 

        public string Feedback { get; set; }
        [Required]
        public List<StudentAttendanceDTO> Students { get; set; }
        [Required]
        public string Teacher_id { get; set; }

    }
}
