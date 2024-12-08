using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.DTOs.ScheduleDTOs
{
    public class SubjectDayScheduleDTO
    {
        public string subject_Name { get; set; }
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
