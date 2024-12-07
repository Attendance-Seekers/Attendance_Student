using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.TimeTableDTO
{
    public class EditTimeTableDTO:AddTimeTableDTO
    {
        [Required]
        public int TimeTableId {  get; set; }

    }
}
