using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Subject
{
    public class SelectSubjectDTO
    {
       
        public int subject_Id { get; set; }
        
        public string subject_Name { get; set; }

        public int subject_Duration { get; set; }

        //public  List<Teacher> teachers { get; set; } 
        //public  List<SubjectDaySchedule> daysScheduled { get; set; } 
    }
}
