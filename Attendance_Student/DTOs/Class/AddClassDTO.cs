using Attendance_Student.Models;

namespace Attendance_Student.DTOs.Class
{
    public class AddClassDTO
    {

        
        public string Class_Name { get; set; }
        public int Class_Size { get; set; } // max no of student

        public List<Student>? students { get; set; }
        public TimeTable? timeTable { get; set; }

    }
}
