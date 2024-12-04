using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.SubjectDTO
{
    public class AddSubjectDTO
    {
       
        [Required]
        [StringLength(50)]
        public string subject_Name { get; set; }
        [Required]
        public int subject_Duration { get; set; }
        
        //public virtual List<SubjectDaySchedule> daysScheduled { get; set; } = new List<SubjectDaySchedule>();
    }
}
