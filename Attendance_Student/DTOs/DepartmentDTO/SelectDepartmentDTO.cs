using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.DTOs.TeacherDTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Attendance_Student.DTOs.DepartmentDTO
{
    public class SelectDepartmentDTO
    {
    
        public int Id { get; set; }

        public string Name { get; set; }
        public List<CustomSelectTeacherDTO> Teachers { get; set; }
        public List<CustomSelectSubjectDTO> Subjects { get; set; }
    }
}
