using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.TeacherDTO
{
    public class EditTeacherDTO:AddTeacherDTO
    {
        [Required]
        public int TeacherId { get; set; }
    }
}
