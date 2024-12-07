using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;

namespace Attendance_Student.DTOs.AttendanceDTOs
{
    public class SelectAttendaceStudentDTO
    {
        public int Id { get; set; }

        public DateOnly dateAttendance { get; set; }
        public string status { get; set; }

        public string teacher_name { get; set; }

        public string subject_name { get; set; }
    }
}
