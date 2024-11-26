using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Class
    {
        
        [Key]
        public int Class_Id { get; set; }
        // public string Id { get; set; } = Guid.NewGuid().ToString(); // Automatically generate a GUID
        //we can use the above code if we want unique string id as auto-increament in int id
        [Required]
        [MaxLength(50)]
        public string Class_Name { get; set; }
        public int Class_Size { get; set; } // max no of student

      
        public virtual TimeTable timeTable { get; set; } // contain subjects which have teacher list
        public virtual List<Student> students { get; set; } = new List<Student>();
    }
}
