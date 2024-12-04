using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.TeacherDTO
{
    public class SelectTeacherDTO
    {
        
        public string Teacher_fullName { get; set; }

        public string address { get; set; }
        public int age { get; set; }


        public int SubjectId { get; set; }
        //public string Subject_Name { get; set; }
        public int DeptId { get; set; }
        //public string department_name { get; set; }

    }
}
