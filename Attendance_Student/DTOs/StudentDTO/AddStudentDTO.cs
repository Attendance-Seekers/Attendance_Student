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
        public string status { get; set; } = "Active"; // Active or InActive // will be enum

        public int ClassId { get; set; }
        public string? ParentId { get; set; }

        [Required]
        public string username { get; set; } 
        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public string password { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string email { get; set; }
        [Required]
        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$")]

        public string phonenumber { get; set; }


    }
}
