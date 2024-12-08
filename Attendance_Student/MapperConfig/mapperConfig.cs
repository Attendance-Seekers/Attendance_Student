using Attendance_Student.DTOs.AdminDTOs;
using Attendance_Student.DTOs.AttendanceDTOs;
using Attendance_Student.DTOs.ClassDTO;
using Attendance_Student.DTOs.DepartmentDTO;
using Attendance_Student.DTOs.ParentDTOs;
using Attendance_Student.DTOs.ScheduleDTOs;
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
            CreateMap<Class, SelectClassDTO>()
                .ForMember(dest => dest.students, opt => opt.MapFrom(src => src.students))
                .ForMember(dest => dest.timeTable, opt => opt.MapFrom(src => src.timeTable))
                .ReverseMap();
            CreateMap<AddClassDTO, Class>().ReverseMap();
            CreateMap<EditClassDTO, Class>().ReverseMap();
            CreateMap<Class, SelectClassesDTO>().ReverseMap();

            // Subject mappers
            CreateMap<Subject, SelectSubjectDTO>().ReverseMap();
            CreateMap<AddSubjectDTO, Subject>().ReverseMap();
            CreateMap<EditSubjectDTO, Subject>().ReverseMap();

            // TimeTable mappers
            CreateMap<TimeTable, SelectTimeTableDTO>()
                .ForMember(dest => dest.daySchedules, opt => opt.MapFrom(src => src.DaySchedules))
                .ReverseMap();
            CreateMap<AddTimeTableDTO, TimeTable>().ReverseMap();
            CreateMap<EditTimeTableDTO, TimeTable>().ReverseMap();

            // Schedule mappers
            CreateMap<DaySchedule, DayScheduleDTO>()
               .ForMember(dest => dest.subjectDaySchedules, opt => opt.MapFrom(src => src.subjectsScheduled))
               .ReverseMap();
            CreateMap<SubjectDaySchedule, SubjectDayScheduleDTO>()
                .AfterMap((src , dest) =>
                {
                    dest.subject_Name = src.subject.subject_Name;
                }).ReverseMap();

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
