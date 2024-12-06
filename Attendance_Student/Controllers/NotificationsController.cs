using Attendance_Student.DTOs.NotificationDTOs;
using Attendance_Student.DTOs.ParentDTOs;
using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        AttendanceStudentContext _context;
        public NotificationsController(AttendanceStudentContext context) 
        {
            _context = context;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendNotifications(SendNotificationDTO sendNotification)
        {
            if (sendNotification == null) return BadRequest("Invalid notification data");

            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            List<Notification> notifications = new List<Notification>();

            List<Student> sts = new List<Student>();

            foreach(var studentSelectDto in sendNotification.Students)
            {
                Student student = await _context.Students.FindAsync(studentSelectDto.id);

                if (student == null) return BadRequest($"Student with ID {studentSelectDto.id} not found.");

                if(student.parent == null) return BadRequest("Parent information is missing for the student.");

                sts.Add(student);
            }
            //var studentIds = sendNotification.Students.Select(s => s.id).ToList();
            //var students = await _context.Students.Include(s => s.parent).Where(s => studentIds.Contains(s.Id)).ToListAsync();
            var groupedByParent = sts.GroupBy(s => s.parent.Id);

            //List<Student> students = new List<Student>();
            foreach(var group in groupedByParent)
            {
                Parent parent = await _context.Parents.FindAsync(group.Key);
                if (parent == null) return BadRequest("Parent not found.");
                var admin = await _context.Admins.FindAsync(sendNotification.admin_id);
                if (admin == null) return BadRequest("Admin not found.");
                Notification notification = new Notification()
                {
                    Message = sendNotification.Message,
                    Type = sendNotification.Type,
                    sendDate = sendNotification.sendDate == default ? currentDate : sendNotification.sendDate,
                    admin_id = admin.Id,
                    Parent_Id = parent.Id,
                };
                notifications.Add(notification);

            }
            await _context.Notifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();
            List<SelectNotificationDTO> selectNotifications = new List<SelectNotificationDTO>();
            foreach(var notification in notifications)
            {
                List<SelectStudentDTO>  studentDTOs = new List<SelectStudentDTO>();
                SelectParentDTO selectParentDTO = new SelectParentDTO()
                {
                    Id = notification.Parent_Id,
                    Name = notification.Parent.fullname,
                    address = notification.Parent.address,
                };
                foreach(var showStudent in sts)
                {
                    SelectStudentDTO selectStudentDTO = new SelectStudentDTO()
                    {
                        username = showStudent.Student_fullname,
                        status = "Absent"
                    };
                    studentDTOs.Add(selectStudentDTO);
                }
                SelectNotificationDTO selectNotificationDTO = new SelectNotificationDTO()
                {
                   Id = notification.Id,
                   Message = notification.Message,
                   Type = notification.Type,
                   sendDate = notification.sendDate,
                   admin_name = notification.admin.UserName,
                   Parent = selectParentDTO,
                   Students = studentDTOs
                };
                selectNotifications.Add(selectNotificationDTO);
            }
            return Ok(selectNotifications);
        }

            [HttpGet]
            public async Task<IActionResult> RecieveNotifications(string parent_id)
            {
                List<Notification> notifications = await _context.Notifications.Where(n => n.Parent_Id == parent_id).ToListAsync();
                if (!notifications.Any()) return BadRequest("No notifications found.");

                var students = await _context.Students.Where(s => s.ParentId ==  parent_id ).ToListAsync();


                List<SelectNotificationDTO> selectNotifications = new List<SelectNotificationDTO>();
                foreach (var notification in notifications)
                {
                    List<SelectStudentDTO> studentDTOs = new List<SelectStudentDTO>();

                    foreach (var showStudent in students)
                    {
                        var attendance = showStudent.viewAttendances.FirstOrDefault(a => a.attendance.dateAttendance == notification.sendDate);
                        if(attendance == null || attendance.Status != "Present")
                        {
                            SelectStudentDTO selectStudentDTO = new SelectStudentDTO()
                            {
                                username = showStudent.Student_fullname,
                                status = "Absent"
                            };
                            studentDTOs.Add(selectStudentDTO);
                            
                        }
                    }
                    SelectNotificationDTO selectNotificationDTO = new SelectNotificationDTO()
                    {
                        Id = notification.Id,
                        Message = notification.Message,
                        Type = notification.Type,
                        sendDate = notification.sendDate,
                        Students = studentDTOs
                    };
                    selectNotifications.Add(selectNotificationDTO);
                }
                return Ok(selectNotifications);
            }
    }
}
