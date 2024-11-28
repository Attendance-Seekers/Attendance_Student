using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class TeacherAttendance
    {

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Attendance")]
        public int AttendanceId { get; set; }
        public virtual Attendance Attendance { get; set; }
        [Column(TypeName = "date")]

        public DateOnly RecordDate { get; set; }
    }
}
