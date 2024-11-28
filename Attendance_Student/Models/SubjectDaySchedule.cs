using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class SubjectDaySchedule
    {
        public int SubjectId { get; set; }
        public virtual Subject subject { get; set; }
        public int DayScheduleId { get; set; }
        public virtual DaySchedule daySchedule { get; set; }
        [Column(TypeName = "time")]

        public TimeSpan StartTime { get; set; }
        [Column(TypeName = "time")]

        public TimeSpan EndTime { get; set; }
    }
}
