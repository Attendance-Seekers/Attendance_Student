using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Student.Models
{
    public class TimeTable
    {
        [Key]
        [ForeignKey("_class")]
        public int timeTable_Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public virtual List<Subject> subjects { get; set; } = new List<Subject>();// contain teachers

       
        //public int class_id { get; set; } // instead of using 2 keys we'll just use the primary as forigen also because it's one-to-one relation
        
        public virtual Class _class { get; set; }// navigation prop virtual is for lazyLoading




    }
}
