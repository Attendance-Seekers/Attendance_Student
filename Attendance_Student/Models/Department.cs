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
    }
}
