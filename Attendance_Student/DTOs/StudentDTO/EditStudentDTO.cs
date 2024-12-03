using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.StudentDTO
{
    public class EditStudentDTO:AddStudentDTO
    {
        [Required]
        public string Student_Id { get; set; }

    }
}
