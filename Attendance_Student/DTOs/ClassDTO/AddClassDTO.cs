using Attendance_Student.Models;

namespace Attendance_Student.DTOs.ClassDTO
{
    public class AddClassDTO
    {

        
        public string Class_Name { get; set; }
        public int Class_Size { get; set; } // max no of student

        public List<string>? studentsIDs { get; set; }
        //public TimeTable? timeTable { get; set; }

    }
}
