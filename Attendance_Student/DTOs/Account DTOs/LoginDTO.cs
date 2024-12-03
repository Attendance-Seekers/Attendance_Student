using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Account_DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
