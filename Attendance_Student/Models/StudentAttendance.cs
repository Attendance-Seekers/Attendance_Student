using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class StudentAttendance
    {

        [ForeignKey("Student")]
        [Required(ErrorMessage = "Student ID is required.")]
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        [ForeignKey("Attendance")]
        [Required(ErrorMessage = "Attendance ID is required.")]
        public int AttendanceId { get; set; }
        public virtual Attendance Attendance { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}
