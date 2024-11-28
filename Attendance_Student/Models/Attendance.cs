using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
 
        public string Feedback { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Attendance date is required.")]
        public DateOnly dateAttendance { get; set; }
        public virtual List<TeacherAttendance> TeachersAttendance { get; set; } = new List<TeacherAttendance>();
        public virtual List<StudentAttendance> StudentsAttendance { get; set; } = new List<StudentAttendance>();

        [ForeignKey("timeTable")]
        public int TimeTableId { get; set; }
        public virtual TimeTable timeTable { get; set; }
    }
}
