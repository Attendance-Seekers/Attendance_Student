using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.DepartmentDTO
{
    public class EditDepartmentDTO
    {
        [Required]
        public int Id { get; set; }

  
        [MaxLength(100)]
        public string Name { get; set; }
        public virtual bool flagAddOrOverwrite { get; set; } = false; // false : add , true; overwrite

        public List<string> TeachersIDs { get; set; } 
        public  List<int> SubjectsIDs { get; set; } 
    }
}
