using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class TimeTable
    {
        [Key]
        public int TimeTableId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("_class")]
        public int? class_id { get; set; } // instead of using 2 keys we'll just use the primary as forigen also because it's one-to-one relation

        public virtual Class _class { get; set; }// navigation prop virtual is for lazyLoading

        public virtual List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();


    }
}
