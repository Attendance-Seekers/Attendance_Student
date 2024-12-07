using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.ParentDTOs
{
    public class ParentUpdateDto
    {
        [StringLength(50)]
        public string? Fullname { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
