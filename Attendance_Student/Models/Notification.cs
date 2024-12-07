using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required , StringLength(150)]
        public string Message { get; set; }
        [Required ] 
        public string Type { get; set; }
        public DateOnly sendDate { get; set; }
        [Required  , ForeignKey("Parent")]
        public string Parent_Id { get; set; }
        public virtual Parent Parent { get; set; }
        //public virtual List<Student> Students { get; set; } = new List<Student>();
        [Required , ForeignKey("admin")]
        public string admin_id {  get; set; }
        public virtual Admin admin { get; set; }

    }
}
