using Attendance_Student.Models;

namespace Attendance_Student.Repositories
{
    public class ClassFuncRepository
    {
        AttendanceStudentContext _context;
        public ClassFuncRepository(AttendanceStudentContext context)
        {
            _context = context;
        }

        //public async Task<SelectClassDTO> SelectAbsentClass()
        //{
        //    var students = 
        //    return await _context.Classes.Where(c => c.students.)
        //}
    }
}
