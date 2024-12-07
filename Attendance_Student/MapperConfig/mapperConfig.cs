using Attendance_Student.DTOs.AdminDTOs;
using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.DTOs.DepartmentDTO;
using Attendance_Student.DTOs.ParentDTOs;
using Attendance_Student.DTOs.StudentDTO;
using Attendance_Student.DTOs.SubjectDTO;
using Attendance_Student.DTOs.TeacherDTO;
using Attendance_Student.DTOs.TimeTableDTO;
using Attendance_Student.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Attendance_Student.MapperConfig
{
    public class mapperConfig : Profile
    {
        public mapperConfig()
        {
            // Class mappers
            CreateMap<Class, SelectClassDTO>().ReverseMap();
            CreateMap<AddClassDTO, Class>().ReverseMap();
            CreateMap<EditClassDTO, Class>().ReverseMap();

            // Subject mappers
            CreateMap<Subject, SelectSubjectDTO>().ReverseMap();
            CreateMap<AddSubjectDTO, Subject>().ReverseMap();
            CreateMap<EditSubjectDTO, Subject>().ReverseMap();

            // TimeTable mappers
            CreateMap<TimeTable, SelectTimeTableDTO>().ReverseMap();
            CreateMap<AddTimeTableDTO, TimeTable>().ReverseMap();
            CreateMap<EditTimeTableDTO, TimeTable>().ReverseMap();

            // Teacher mappers
            CreateMap<Teacher, SelectTeacherDTO>().ReverseMap();
            CreateMap<AddTeacherDTO, Teacher>().ReverseMap();
            CreateMap<EditTeacherDTO, Teacher>().ReverseMap();

            // Student mappers
            CreateMap<Student, SelectStudentDTO>().ReverseMap();
            CreateMap<AddStudentDTO, Student>().ReverseMap();
            CreateMap<EditStudentDTO, Student>().ReverseMap();

            // Admin mappers
            CreateMap<AddAdminDTO, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<IdentityUser, AdminDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // Parent mappers
            CreateMap<ParentCreateDto, Parent>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            // Department mappers
            CreateMap<Department, SelectDepartmentDTO>().ReverseMap();
            CreateMap<AddDepartmentDTO, Department>().ReverseMap();
            CreateMap<EditDepartmentDTO, Department>().ReverseMap();
            CreateMap<Teacher, CustomSelectTeacherDTO>().ReverseMap();
            CreateMap<Subject, CustomSelectSubjectDTO>().ReverseMap();

            // Additional Parent-Student mappers
            CreateMap<Parent, ParentResponseDto>();
            CreateMap<ParentUpdateDto, Parent>();

            CreateMap<Parent, ParentProfileDto>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

            CreateMap<Student, ParentStudentDto>()
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src._class.Class_Name ?? "Not Assigned"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status.ToString()));
        }
    }
}
