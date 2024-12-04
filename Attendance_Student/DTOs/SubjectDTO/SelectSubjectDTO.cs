using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.SubjectDTO
{
    public class SelectSubjectDTO
    {
       
        public int subject_Id { get; set; }
        
        public string subject_Name { get; set; }

        public int subject_Duration { get; set; }

        public  List<SelectTeacherDTO> teachers { get; set; }
        //public  List<SubjectDaySchedule> daysScheduled { get; set; } 
    }
}
