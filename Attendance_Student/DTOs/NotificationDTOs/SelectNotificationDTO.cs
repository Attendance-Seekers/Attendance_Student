using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Attendance_Student.DTOs.ParentDTOs;
using Attendance_Student.DTOs.StudentDTO;

namespace Attendance_Student.DTOs.NotificationDTOs
{
    public class SelectNotificationDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Type { get; set; }
        public DateOnly sendDate { get; set; }
        public virtual SelectParentDTO Parent { get; set; }
        public string admin_name { get; set; }
        public virtual List<SelectStudentDTO> Students { get; set; }

    }
}
