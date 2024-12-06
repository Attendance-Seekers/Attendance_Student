using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.DepartmentDTO
{
    public class AddDepartmentDTO
    {
      

        [MaxLength(100)]
        public string Name { get; set; }

        public  List<string>? TeachersIDs { get; set; }
        public  List<int>? SubjectsIDs { get; set; }
    }
}
