using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.TeacherDTO
{
    public class SelectTeacherDTO:EditTeacherDTO
    {
        
        public int DeptId { get; set; }
        //public string department_name { get; set; }

    }
}
