using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Models
{
    public class AttendanceStudentContext:IdentityDbContext
    {
        public AttendanceStudentContext(DbContextOptions<AttendanceStudentContext> options):base(options)
        {
                
        }
    }
}
