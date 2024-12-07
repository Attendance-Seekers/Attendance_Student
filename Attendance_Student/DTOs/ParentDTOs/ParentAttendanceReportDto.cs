namespace Attendance_Student.DTOs.ParentDTOs
{
    public class ParentAttendanceReportDto
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<AttendanceRecordDto> AttendanceRecords { get; set; }
        public decimal AttendancePercentage { get; set; }
    }
}
