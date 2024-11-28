using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.Models
{
    public class Parent:IdentityUser
    {
        [Required , StringLength (50)]
        public string fullname { get; set; }
        public int age { get; set; }
        [Required , StringLength (200)]
        public string address { get; set; }
        public virtual List<Student> Students { get; set; } = new List<Student>();
    }
}
