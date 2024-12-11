using Attendance_Student.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using System.Globalization;

namespace Attendance_Student.DTOs.StudentDTO
{
    public class SelectStudentDTO
    {
        [Ignore]
        public string id { get; set; }
        public string username { get; set; }
        public string status { get; set; }
        public string class_name { get; set; }
        public int age { get; set; }
        public string parent_name { get; set; }


    }
}
