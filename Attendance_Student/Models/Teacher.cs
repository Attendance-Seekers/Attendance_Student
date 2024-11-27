using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Teacher
    {
        [Key] 
        public int Id { get; set; }
        [MaxLength(100)] 
        public string Name { get; set; }

        [Required]
        [MaxLength(200)] 
        public string Address { get; set; }
        //public virtual List<Class> Classes { get; set; } = new List<Class>();

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        [ForeignKey("Department")] 
        public int DeptId { get; set; }
        public virtual Department Department { get; set; }
    }
}
