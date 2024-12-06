using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Attendance_Student.DTOs.StudentDTO;

namespace Attendance_Student.DTOs.NotificationDTOs
{
    public class SendNotificationDTO
    {
        [Required, StringLength(150)]
        public string Message { get; set; }
        [Required]
        public string Type { get; set; }
        public DateOnly sendDate { get; set; }
        public virtual List<SelectStudentDTO> Students { get; set; }
        public string admin_id { get; set; }
    }
}
