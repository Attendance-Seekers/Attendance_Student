using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Account_DTOs
{
    public class RegisterDTO
    {
       
     

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

    
        [Phone]
        public string Phone { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
