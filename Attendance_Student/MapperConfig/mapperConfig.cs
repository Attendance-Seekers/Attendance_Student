using Attendance_Student.DTOs.AttendanceDTOs;
using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.DTOs.DepartmentDTO;
using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.DTOs.TimeTableDTO;
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

            //    CreateMap<Student, SelectStudentDTO>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            //    CreateMap<AddStudentDTO, Student>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //    CreateMap<EditStudentDTO, Student>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //DEpartment mappers
            CreateMap<Department, SelectDepartmentDTO>().ReverseMap();
            CreateMap<AddDepartmentDTO, Department>().ReverseMap();
            CreateMap<EditDepartmentDTO, Department>().ReverseMap();
            CreateMap<Teacher, CustomSelectTeacherDTO>().ReverseMap();
            CreateMap<Subject, CustomSelectSubjectDTO>().ReverseMap();

          

            // Mapping for SelectAttendaceStudentDTO from StudentAttendance
            CreateMap<StudentAttendance, SelectAttendaceStudentDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
           .ForMember(dest => dest.dateAttendance, opt => opt.MapFrom(src => src.attendance.dateAttendance))
           .ForMember(dest => dest.teacher_name, opt => opt.MapFrom(src => src.attendance.teacher.Teacher_fullName))
           .ForMember(dest => dest.subject_name, opt => opt.MapFrom(src => src.attendance.teacher.Subject.subject_Name))
           .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));


            // Mapping for SelectStudentDTO from StudentAttendance
            CreateMap<StudentAttendance, SelectStudentDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.student != null ? src.student.UserName : ""));

            // Mapping for SelectAttendanceDTO from Attendance
            CreateMap<Attendance, SelectAttendanceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.dateAttendance, opt => opt.MapFrom(src => src.dateAttendance))
                .ForMember(dest => dest.teacher_name, opt => opt.MapFrom(src => src.teacher.Teacher_fullName))
                .ForMember(dest => dest.subject_name, opt => opt.MapFrom(src => src.teacher.Subject.subject_Name))
                .ForMember(dest => dest.StudentsAttendance, opt => opt.MapFrom(src => src.StudentsAttendance));
        
            // Mapping from StudentAttendanceDTO to StudentAttendance
            CreateMap<StudentAttendanceDTO, StudentAttendance>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId));

            // Mapping from AttendanceDTO to Attendance
            CreateMap<AttendanceDTO, Attendance>()
                .ForMember(dest => dest.dateAttendance, opt => opt.MapFrom(src => src.DateAttendance))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback))
                .ForMember(dest => dest.teacher_id, opt => opt.MapFrom(src => src.Teacher_id))
                .ForMember(dest => dest.StudentsAttendance, opt => opt.MapFrom(src => src.Students));

    

    

        }



    }
}
