using Attendance_Student.DTOs.Class;
using Attendance_Student.DTOs.Subject;
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
        }

    }
}
