using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class DaySchedule
    {
        [Key]
        public int DayScheduleId { get; set; }
        [Required]
        public string Dayname { get; set; }
        [ForeignKey("TimeTable")]
        public int TimeTable_id { get; set; }
        public virtual TimeTable TimeTable { get; set; }
        public virtual List<SubjectDaySchedule> subjectsScheduled { get; set; } = new List<SubjectDaySchedule>();// contain teachers
    }
}
