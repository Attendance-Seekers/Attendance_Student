using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Class
{
    public class EditClassDTO:AddClassDTO
    {
        [Required]
        public int Class_Id { get; set; }

    }
}
