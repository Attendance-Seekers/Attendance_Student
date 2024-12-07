using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.ParentDTOs
{
    public class ParentCreateDto
    {
        //[Required, StringLength(50)]
        public string Fullname { get; set; }

        [Required, Range(22, 100)]
        public int Age { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Role added for role-based registration

        [Required]
        public string Username { get; set; } // Username added for user creation
    }
}
