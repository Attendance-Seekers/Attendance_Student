using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.StudentDTO
{
    public class AddStudentDTO
    {
        [Required]  
        public string Student_fullname { get; set; }
        [Range(5, 17, ErrorMessage = "Age must be between 5 and 17 years.")]

        public int age { get; set; }
        //public string status { get; set; }

        public int ClassId { get; set; }
        public string ParentId { get; set; }
       

    }
}
