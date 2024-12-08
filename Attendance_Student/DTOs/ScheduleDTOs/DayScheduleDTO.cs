namespace Attendance_Student.DTOs.ScheduleDTOs
{
    public class DayScheduleDTO
    {
        public string Dayname { get; set; }
        public List<SubjectDayScheduleDTO> subjectDaySchedules { get; set; }
    }
}
