using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Subject
{
    public class EditSubjectDTO:AddSubjectDTO
    {
        [Required]
        public int subject_Id { get; set; }
    }
}
