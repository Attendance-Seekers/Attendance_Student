using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Account_DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters long")]
        public string NewPassword { get; set; }
    }
}
