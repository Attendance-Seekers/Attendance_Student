using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Teacher:IdentityUser
    {

        [MaxLength(100)]
        public string Teacher_fullName { get; set; }

        [Required]
        [MaxLength(200)]
        public string address { get; set; }
        [Range(24, 70, ErrorMessage = "Age must be between 24 and 70 years.")]
        public int age { get; set; }


        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        [ForeignKey("department")]
        public int? DeptId { get; set; }
        public virtual Department department { get; set; }

        public virtual List<TeacherAttendance> AttendanceRecords { get; set; } = new List<TeacherAttendance>();
    }
}
