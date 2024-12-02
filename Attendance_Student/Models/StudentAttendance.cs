using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class StudentAttendance
    {

        [ForeignKey("student")]
        //[Required(ErrorMessage = "Student ID is required.")]
        public string StudentId { get; set; }

        public virtual Student student { get; set; }

        [ForeignKey("attendance")]
        //[Required(ErrorMessage = "Attendance ID is required.")]
        public int AttendanceId { get; set; }
        public virtual Attendance attendance { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}
