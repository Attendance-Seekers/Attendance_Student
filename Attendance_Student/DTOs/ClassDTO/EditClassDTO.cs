using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.ClassDTO
{
    public class EditClassDTO:AddClassDTO
    {
        [Required]
        public int Class_Id { get; set; }

        public virtual bool flagAddOrOverwrite { get; set; } = false; // false : add , true; overwrite

    }
}
