using Attendance_Student.DTOs.Class;
using Attendance_Student.DTOs.Student;
using Attendance_Student.DTOs.Subject;
using Attendance_Student.DTOs.Teacher;
using Attendance_Student.DTOs.TimeTable;
using Attendance_Student.Models;
using AutoMapper;

namespace Attendance_Student.MapperConfig
{
    public class mapperConfig : Profile 
    {

        public mapperConfig()
        {
            //class mappers
            CreateMap< Class, SelectClassDTO>().ReverseMap();
            CreateMap<AddClassDTO,Class>().ReverseMap();
            CreateMap<EditClassDTO,Class>().ReverseMap();
            // subject mappers
            CreateMap<Subject, SelectSubjectDTO>().ReverseMap();
            CreateMap<AddSubjectDTO, Subject>().ReverseMap();
            CreateMap<EditSubjectDTO, Subject>().ReverseMap();
            //TimeTable mappers
            CreateMap<TimeTable, SelectTimeTableDTO>().ReverseMap();
            CreateMap<AddTimeTableDTO, TimeTable>().ReverseMap();
            CreateMap<EditTimeTableDTO, TimeTable>().ReverseMap();
            //Teacher mappers
            CreateMap<Teacher, SelectTeacherDTO>().ReverseMap();
            CreateMap<AddTeacherDTO, Teacher>().ReverseMap();
            CreateMap<EditTeacherDTO, Teacher>().ReverseMap();
            //Student mappers
            CreateMap<Student, SelectStudentDTO>().ReverseMap();
            CreateMap<AddStudentDTO, Student>().ReverseMap();
            CreateMap<EditStudentDTO, Student>().ReverseMap();
        }



    }
}
