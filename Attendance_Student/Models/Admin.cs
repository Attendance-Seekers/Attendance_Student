using Microsoft.AspNetCore.Identity;

namespace Attendance_Student.Models
{
    public class Admin:IdentityUser
    {
        public virtual List<Notification> Notifications { get; set; }
    }
}
