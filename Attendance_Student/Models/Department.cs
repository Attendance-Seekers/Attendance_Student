using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.Models
{
    public class Department
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Name { get; set; }

        public virtual List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual List<Subject> Subjects { get; set; } = new List<Subject>(); // one dept have sevral subjects 
    }
}
