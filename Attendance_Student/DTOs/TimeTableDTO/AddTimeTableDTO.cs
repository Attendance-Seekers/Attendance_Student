namespace Attendance_Student.DTOs.TimeTableDTO
{
    public class AddTimeTableDTO
    {
        //public int TimeTableId { get; set; }
        //public DateTime CreatedDate { get; set; }

        public int class_id { get; set; } // instead of using 2 keys we'll just use the primary as forigen also because it's one-to-one relation

        public string? class_name { get; set; }// navigation prop virtual is for lazyLoading

        //public virtual List<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();
    }
}
