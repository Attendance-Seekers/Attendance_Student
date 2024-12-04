using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.TeacherDTO
{
    public class EditTeacherDTO
    {
        [Required]
        public string Id { get; set; }
        public string Teacher_fullName { get; set; }

        public string address { get; set; }
        public int age { get; set; }


        public int SubjectId { get; set; }
        //public string? Subject_Name { get; set; }
        //public int DeptId { get; set; }
        //public string? department_name { get; set; }
        
        
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string email { get; set; }
        [RegularExpression("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$")]

        public string phonenumber { get; set; }

    }
}
