using Attendance_Student.Models;

namespace Attendance_Student.Repositories
{
    public class AttendanceRepository
    {
        AttendanceStudentContext _db;
        public AttendanceRepository(AttendanceStudentContext db)
        {
            _db = db;
        }
        
    }
}
