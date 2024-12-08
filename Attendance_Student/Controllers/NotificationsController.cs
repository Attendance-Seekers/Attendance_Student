using Attendance_Student.DTOs.NotificationDTOs;
using Attendance_Student.DTOs.ParentDTOs;
using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.Models;
using Attendance_Student.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Attendance_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        UnitWork _unit;
        public NotificationsController(UnitWork unit) 
        {
            _unit = unit;
        }
        [HttpPost("send")]
        [SwaggerOperation(Summary = "Send notifications to parents", Description = "Sends notifications to parents regarding their students' attendance status.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notifications sent successfully.", Type = typeof(List<SelectNotificationDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid notification data or student/parent/admin not found.")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> SendNotifications(SendNotificationDTO sendNotification)
        {
            if (sendNotification == null) return BadRequest("Invalid notification data");

            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            List<Notification> notifications = new List<Notification>();

            List<Student> sts = new List<Student>();

            foreach(var studentSelectDto in sendNotification.Students)
            {
                Student student = await _unit.StudentRepo.selectUserById(studentSelectDto.id);

                if (student == null) return BadRequest($"Student with ID {studentSelectDto.id} not found.");

                if(student.parent == null) return BadRequest("Parent information is missing for the student.");

                sts.Add(student);
            }

            var groupedByParent = sts.GroupBy(s => s.parent.Id);


            foreach(var group in groupedByParent)
            {
                Parent parent = await _unit.ParentRepo.selectUserById(group.Key);
                if (parent == null) return BadRequest("Parent not found.");
                var admin = await _unit.AdminReop.selectUserById(sendNotification.admin_id);
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
            await _unit.NotificationFuncRepository.AddNotifications(notifications);
            await _unit.Save();
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
        [SwaggerOperation(Summary = "Receive notifications for a parent", Description = "Retrieves notifications for a specific parent and includes the attendance status of their children.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notifications received successfully.", Type = typeof(List<SelectNotificationDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "No notifications found for the given parent ID.")]
        [Produces("application/json")]
        public async Task<IActionResult> ReceiveNotifications(string parent_id)
            {
                List<Notification> notifications = await _unit.NotificationFuncRepository.GetNotificationOfParent(parent_id);
                if (!notifications.Any()) return BadRequest("No notifications found.");

                var students = await _unit.NotificationFuncRepository.GetStudentsFromParent(parent_id);


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
