using System.ComponentModel.DataAnnotations;

namespace Attendance_Student.DTOs.Class
{
    public class ClassDTO
    {
        public int Class_Id { get; set; }

        public string Class_Name { get; set; }
        public int Class_Size { get; set; } // max no of student


    }
}
