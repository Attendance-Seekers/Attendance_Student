using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.AttendanceDTOs
{
    public class StudentAttendanceDTO
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string status { get; set; }
    }
}
