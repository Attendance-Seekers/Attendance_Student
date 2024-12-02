using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class TeacherAttendance
    {

        [ForeignKey("teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher teacher { get; set; }

        [ForeignKey("attendance")]
        public int AttendanceId { get; set; }
        public virtual Attendance attendance { get; set; }
        [Column(TypeName = "date")]

        public DateOnly RecordDate { get; set; }
    }
}
