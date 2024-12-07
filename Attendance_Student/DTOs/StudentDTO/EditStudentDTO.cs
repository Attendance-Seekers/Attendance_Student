using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.StudentDTO
{
    public class EditStudentDTO
    {
        [Required]
        public string Id { get; set; }
        
        public string Student_fullname { get; set; }
        [Range(5, 17, ErrorMessage = "Age must be between 5 and 17 years.")]

        public int age { get; set; }
        public Status status { get; set; }
        public int ClassId { get; set; }
        public string? ParentId { get; set; }

        //public string username { get; set; }
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string email { get; set; }

        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$")]
        public string phonenumber { get; set; }

    }
}
