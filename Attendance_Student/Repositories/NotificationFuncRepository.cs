using Attendance_Student.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Repositories
{
    public class NotificationFuncRepository
    {
        AttendanceStudentContext _context;
        public NotificationFuncRepository(AttendanceStudentContext context)
        {
            _context = context;
        }
        public async Task AddNotifications(List<Notification> notifications)
        {
            await _context.Notifications.AddRangeAsync(notifications);
        }
        public async Task<List<Notification>> GetNotificationOfParent(string parent_id)
        {
            return await _context.Notifications.Where(n => n.Parent_Id == parent_id).ToListAsync();
        }
        public async Task<List<Student>> GetStudentsFromParent(string parent_id)
        {
                return await _context.Students.Where(s => s.ParentId ==  parent_id ).ToListAsync();
        }
        
    }
}
