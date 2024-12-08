using Attendance_Student.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Repositories
{
    public class AttendanceFuncRepository
    {
        AttendanceStudentContext _context;
        public AttendanceFuncRepository(AttendanceStudentContext context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetAttendancesDay(int class_id, DateOnly date)
        {
            return await _context.Attendances.Where(a => a.dateAttendance == date && a.timeTable.class_id == class_id).ToListAsync();
        }
        public async Task<List<Attendance>> GetAttendancesClass(int class_id, DateOnly start_date, DateOnly end_date)
        {
            return await _context.Attendances.Where(a => a.dateAttendance >= start_date && a.dateAttendance <= end_date && a.timeTable.class_id == class_id).ToListAsync();
        }
        public async Task<List<StudentAttendance>> GetAttendanceStudent(string st_id)
        {
            return await _context.StudentAttendances.Where(s => s.StudentId == st_id).ToListAsync();
        }
        public async Task<List<StudentAttendance>> GetAttendanceStudentRange(string st_id, DateOnly start_date, DateOnly end_date)
        {
            return await _context.StudentAttendances.Where(s => s.StudentId == st_id && s.attendance.dateAttendance >= start_date && s.attendance.dateAttendance <= end_date).ToListAsync();
        }


    }
}
