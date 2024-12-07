using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Attendance_Student.DTOs.TimeTableDTO;
using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.DTOs.StudentDTO;

namespace Attendance_Student.DTOs.AttendanceDTOs
{
    public class SelectAttendanceDTO
    {
        public int Id { get; set; }

        public string Feedback { get; set; }
   
        public DateOnly dateAttendance { get; set; }
        public virtual List<SelectStudentDTO> StudentsAttendance { get; set; }

        public string teacher_name { get; set; }

        public string subject_name { get; set; }
        
    }
}
