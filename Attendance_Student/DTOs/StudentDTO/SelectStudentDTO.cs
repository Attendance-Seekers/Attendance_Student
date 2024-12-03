using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.StudentDTO
{
    public class SelectStudentDTO
    {
      
        public string Student_fullname { get; set; }
        public int age { get; set; }
        public string status { get; set; }

        public int ClassId { get; set; }
        public string Class_Name { get; set; }
        public string ParentId { get; set; }
        public string  Parent_fullname { get; set; }
        
    }
}
